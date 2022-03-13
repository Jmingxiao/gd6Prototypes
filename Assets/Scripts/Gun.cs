using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]Enemy enemy;
    Transform player;
    Collider2D col;
    float shootingInterval =2.0f;
    float timer =0.0f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
         timer+= Time.deltaTime;
         player = GameObject.FindGameObjectWithTag("Player").transform;
         col = GetComponent<Collider2D>();
    }
    private void FixedUpdate() {
        if(timer>shootingInterval&&col.bounds.Contains(player.position)){
             Debug.Log("check");
            timer =0.0f;
            var dir = (player.position- transform.position).normalized;
            if(!Physics2D.Linecast(transform.position,player.position,LayerMask.GetMask("Ground"))){
                enemy.inrange  = true;
            }
        }
    }
}
