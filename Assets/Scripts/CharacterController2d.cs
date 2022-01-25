using UnityEngine;

public class CharacterController2d : MonoBehaviour
{
    private void Start() {
        transform.position = new Vector3();
    }    
    void Update()
    {
        Move();
    }

    void Move(){

        if(Input.GetKeyDown(KeyCode.LeftArrow)&&transform.position.x>-Config.SCRWidth){
            transform.position+= Vector3.left;
        }
         if(Input.GetKeyDown(KeyCode.RightArrow)&&transform.position.x<Config.SCRWidth){
            transform.position+= Vector3.right;
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
