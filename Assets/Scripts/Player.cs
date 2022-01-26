using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private static int score = 0;
    private static int life = 3;
    
    AudioSource audiosource;
    [SerializeField] AudioClip moveSound;
    [SerializeField] AudioClip catchSound;

    [SerializeField]public Text scoreText;

    private void Start() {
        transform.position = new Vector3(.0f,-Config.SCRHeight,.0f);
        audiosource = GetComponent<AudioSource>();
    }   
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("candy")){
            score++;
            CandySpawner.AccelarateSpawnRate();
            audiosource.clip = catchSound;            
            audiosource.Play();

        }
    }
    private void Update() {
        
        TextDisplay();
        if(Move()){
            audiosource.clip = moveSound;
            audiosource.Play();
        }
    }
    
    public static void LossLife(){
        if(life>0){
            life--; 
        }else{
            CandySpawner.spawning=false;
        }
    }

    bool Move(){
        var ismoving =false;
        if(Input.GetKeyDown(KeyCode.LeftArrow)&&transform.position.x>-Config.SCRWidth){
            transform.position+= Vector3.left;
            ismoving =true;
        }
         if(Input.GetKeyDown(KeyCode.RightArrow)&&transform.position.x<Config.SCRWidth){
            transform.position+= Vector3.right;
            ismoving =true;
        }
        return ismoving;
    }
    void TextDisplay(){
        if(life>0){
            scoreText.text = "Score: "+ score.ToString() + "  Life: "+ life.ToString();
        }else{
            scoreText.text = "You Loss!!!" + " Score: "+score.ToString(); 
        }
    }




    /*void Jump(){
        bool grounded = Physics2D.Raycast(transform.position,Vector2.down,distanceOffset,ground);
        if(grounded&&playerjump){
            rb.AddForce(Vector2.up*speed,ForceMode2D.Impulse);
        }
        playerjump =false;
    }*/

}
