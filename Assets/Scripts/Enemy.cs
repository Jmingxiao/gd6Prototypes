using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   Transform player;
    [SerializeField ]Transform gun;
    ObjectPool objectPool ;
    [HideInInspector]public bool inrange;
    Animator animator;
    int health = 2;

    // Start is called before the first frame update
    private void Start() {
        objectPool = ObjectPool.Instance; 
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {   
       
        if(health ==0){
            health--;
            animator.SetTrigger("dead");
            animator.SetBool("death",true);
        }
        if(health>0){
            if(inrange){
                var angle = Vector2.Angle(gun.position,player.position);
                var bullet =objectPool.SpawnFromPool("Bullet",gun.position,Quaternion.identity).GetComponent<Bullet>();
                bullet.dir = (player.position- transform.position).normalized;
                animator.SetTrigger("attack");

            }
            inrange =false;
        }
         
    }
    public void GotHarm(){
        health--;
        animator.SetTrigger("hurt");
    }
}
