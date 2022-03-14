using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   Transform player;
    [SerializeField ]Gun gun;
    ObjectPool objectPool ;
    [HideInInspector]public bool inrange;
    Animator animator;
    int health = 3;
    float turnInterval =4.0f;
    Rythmcontrol rythmcontrol;
    bool dead =false;

    // Start is called before the first frame update
    private void Start() {
        objectPool = ObjectPool.Instance; 
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rythmcontrol = GameObject.FindGameObjectWithTag("beat").GetComponent<Rythmcontrol>();
    }

    // Update is called once per frame
    void Update()
    {   
       
        if(!dead){
            if(health <=0){
                dead =true;
                animator.SetTrigger("dead");
                animator.SetBool("death",dead);
                return;
            }
            turnaround();
            if(inrange&&rythmcontrol.bass){
                var angle = Vector2.Angle(gun.transform.position,player.position);
                var bullet =objectPool.SpawnFromPool("Bullet",gun.transform.position,Quaternion.identity).GetComponent<Bullet>();
                bullet.dir = (player.position- transform.position).normalized;
                animator.SetTrigger("attack");
                gun.timer =0.0f;
            }
            inrange =false;
        }
         
    }
    public void GotHarm(){
        health--;
        animator.SetTrigger("hurt");
    }
    public void turnaround(){
        if(turnInterval<=0){
            turnInterval =4.0f;
            transform.Rotate(Vector3.up*180);
        }
        turnInterval-=Time.deltaTime;
    }
}
