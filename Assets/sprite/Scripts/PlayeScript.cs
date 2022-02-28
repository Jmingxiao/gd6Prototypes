using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeScript : MonoBehaviour
{
    [SerializeField]float speed =0.5f;
    Animator animator;
    float lastx,lasty;
    public static bool getnotice =false;
    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if(getnotice){
            Move();
        }

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
}
