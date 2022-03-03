using UnityEngine;
using UnityEngine.UI;

public class PlayeScript : MonoBehaviour
{
    [HideInInspector]public bool sitting;
    [SerializeField]float speed =0.5f;
    Animator animator;
    float lastx,lasty;
    public static bool getnotice =false;

    float dreadtimmer = 20;
    public static bool dread;
    public Image darkness;
    public GameObject phone;
    public GameObject bubble;

    [SerializeField]AudioClip dreadsound;
    [SerializeField]AudioClip backmusic;
    [SerializeField]Transform Door;
    [SerializeField]Transform nurse;
    [SerializeField] GameObject finish;

    AudioSource audioSource;

    Node m_node;
    Grid grid;
    float timer;
    bool dreadful =false;
    static Vector3 currentPos = new Vector3(6.0f,3.4f);
    bool endgame =false;

    private void Start() {
        
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        resetparameters();
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
        grid.path.FindPath(transform.position,Door.position);
        transform.position = currentPos;
        bubble.SetActive(false);
        finish.SetActive(false);
    }

    private void Update() {
        var realtime =Time.realtimeSinceStartup;
        if(getnotice&&!dreadful&&realtime<300){
            Move();
            DreadUpdate();
            if(Input.GetKeyDown(KeyCode.P)){
                phone.SetActive(!phone.activeSelf);
            }
            if (dreadtimmer < 0)
            {   
                audioSource.clip = dreadsound;
                if(!audioSource.isPlaying){audioSource.Play();}
                timer+= Time.deltaTime;
                if(timer<0.7f){
                    darkness.GetComponent<Image>().color = new Color(1,1,1,timer);
                }
                if(timer>Config.dreadDuration){
                    grid.path.FindPath(transform.position,Door.position);
                   dreadful =true;
                }  
            }
        }else if(dreadful){
            movetoTarget(Door.position);
        }else if(realtime>=300){
            if(!endgame){
                grid.path.FindPath(transform.position,nurse.position);
                endgame =true;
            }
            movetoTarget(nurse.position);
            if((nurse.position-transform.position).magnitude<0.5f){
                finish.SetActive(true);
                var allaudio = FindObjectsOfType<AudioSource>();
                foreach(var audio in allaudio){
                    audio.Stop();
                }
            }

        }
        currentPos = transform.position;
    }

    void Move(){
        Vector3 x = Vector3.right* Input.GetAxis("Horizontal");   
        Vector3 y = Vector3.up*Input.GetAxis("Vertical");
        Vector3 heading = Vector3.Normalize(x+y);   
        transform.position += heading*speed*Time.deltaTime;
        UPdateAnimation(heading);
    }
    void UPdateAnimation(Vector3 dir){
        if(dir.x== 0f&&dir.y==0f){
            animator.SetFloat("LastDirx",lastx);
            animator.SetFloat("LastDiry",lasty);
            animator.SetBool("walk",false);
        }else{
            lastx = dir.x;
            lasty = dir.y;
            animator.SetBool("walk",true);
            animator.SetFloat("Dirx",dir.x);
            animator.SetFloat("Diry",dir.y);
        }
    }

    void DreadUpdate(){
        if(dread)
        {
            dreadtimmer -= Time.deltaTime;
        }else{
            resetparameters();
        }
        dread =true;
    }
    void resetparameters(){
        dreadtimmer = Config.dreadDuration;
        darkness.color = new Color32(255, 255, 255, 0);
        timer = 0;
        dread =true;
        audioSource.clip = backmusic;
        if(!audioSource.isPlaying){audioSource.Play();}

        dreadful =false;
    }
    void movetoTarget(Vector3 tar){
        if((tar-transform.position).magnitude>1.0f){
            m_node = grid.NodeFromWorldPoint(transform.position);
            if (m_node.cell != null)
            {
                Debug.Log(m_node.cell.worldPos);
                transform.position = Vector3.MoveTowards(transform.position, m_node.cell.worldPos, Time.deltaTime);
                var dir = (m_node.cell.worldPos- transform.position).normalized;
                UPdateAnimation(dir);
            }
            if (grid.path.m_path.Contains(m_node) && m_node.cell != null)
            {
                grid.path.m_path.Remove(m_node);
            }
        }else{
             transform.position = Vector3.MoveTowards(transform.position, tar, Time.deltaTime);
        }
    }
}
