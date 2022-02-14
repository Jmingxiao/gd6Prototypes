using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class Player : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] GameObject winloss;
    [HideInInspector]public Vector3 target;
    [HideInInspector]public int currentTask=0;
    [HideInInspector]public float bubboTimer= 0.0f;
    [HideInInspector]public float anxietyPoint =0.0f;
    [HideInInspector]public bool firstround=true;
    [HideInInspector]public Animator animator;

    [SerializeField] GameObject waterbottle;
    [SerializeField] GameObject Key;
    [SerializeField]AudioClip footsound;
    public Door room;
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    public List<Task> tasks;
    public State playerstate
    {
        get { return currentState; }
    }
    private State currentState;
    public readonly IdleState idle = new IdleState();
    public readonly Walkstate walk = new Walkstate();
    public readonly ProcessState process = new ProcessState();
    public readonly GoBackRoom backRoom = new GoBackRoom();

    
    private void Start() {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Key.SetActive(false);
        waterbottle.SetActive(false);
        foreach(var t in tasks){
            t.init();
        }
        TransitionToState(idle);
        bubboTimer =Config.bubbointer;
        winloss.SetActive(false);
    }
    private void Update() {
       currentState.Update(this);
       if(room.onClick()&&!firstround&&currentState!=process){
           TransitionToState(backRoom);
       } 
       image.fillAmount = anxietyPoint/Config.maxAnxiety;
       if(anxietyPoint>Config.maxAnxiety){
           winloss.SetActive(true);
       }      
    }

    public void TransitionToState(State state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
    public bool bubboUpdate(){
        tasks[currentTask].onBubbo(transform);
        if(tasks[currentTask].OnClickBubbo()){
            bubboTimer=0.0f;
            return true;
        }
        return false;
    }
    public void SetTarget(){
        target = tasks[currentTask].targetDestination.position;
        transform.eulerAngles=target.x-transform.position.x<0?new Vector3(0,180,0):Vector3.zero;
        animator.SetBool("walk",true);
    }
    public void roomTarget(){
        target = room.transform.position;
        transform.eulerAngles=target.x-transform.position.x<0?new Vector3(0,180,0):Vector3.zero;
        animator.SetBool("walk",true);
    }
    public bool MovetoTarget(){
        var des = new Vector3(target.x,transform.position.y);
        transform.position = Vector3.MoveTowards(transform.position,des,Time.deltaTime*3.0f);
        return (transform.position-des).magnitude<=0.1f;
    }
    public void switchTask(){
        if(!firstround&&tasks.Count>=3){
            currentTask += Random.Range(1,tasks.Count);
        }else{
            if(tasks.Count-1==currentTask){
                firstround=false;
            }
            GetCurrentTask().completeTimes++;
            currentTask++;
        }
            currentTask = currentTask%tasks.Count;
    }
    public Task GetCurrentTask(){
        
        if(tasks.Count>currentTask){
            return tasks[currentTask];
        }
        return null;
    }

    public void TaskUpdate(bool start){
          switch(GetCurrentTask().name){
            case "waterPlant":
                waterbottle.SetActive(start);
            break;
            case "lockDoor":
                Key.SetActive(start);
            break;
            case "washHands":
                StartCoroutine(fade(start));
                Debug.Log(10);
            break;
            default:
            break;
        }
    }
    public void playCurrentTaskAudio(){
       audioSource.clip = GetCurrentTask().audio;
       audioSource.Play();
    }
    IEnumerator fade(bool inout){
        if(inout){
            var alpha = 1.0f;
            while(alpha>0){
                alpha-= Time.deltaTime;
                spriteRenderer.color = new Color(1,1,1,alpha);
                yield return null;
            }
        }else{
            var alpha = 0.0f;
            while(alpha<1){
                alpha+= Time.deltaTime;
                spriteRenderer.color = new Color(1,1,1,alpha);
                yield return null;
            }
        }
    }
    public void fadeoutdoor(){
         StartCoroutine(fade(true));
    }
    public void playwalksound(bool play){
        if(play){
            audioSource.clip = footsound;
            audioSource.loop =true;
            audioSource.Play();
        }else{
            audioSource.Stop();
            audioSource.loop =false;
        }
    }
}
