using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]Enemy enemy;
    [SerializeField ]Transform player;
    
    float shootingInterval =0.5f;
    float timer =0.0f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
         timer+= Time.deltaTime;
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player")&&timer>shootingInterval){
            timer =0.0f;
            var dir = (player.position- transform.position).normalized;
            if(!Physics2D.Linecast(transform.position,player.position,LayerMask.GetMask("Ground"))){
                enemy.inrange  = true;
            }
        }
    }
}
