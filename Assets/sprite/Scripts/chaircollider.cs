using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chaircollider : MonoBehaviour
{ 
   
    public Vector3 direction;
    bool inrange;
    GameObject player;
    // Start is called before the first frame update

    private void Update() {
      
    }
   private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            player = other.gameObject;
            inrange =true;
        }   
   }
   private void OnTriggerExit2D(Collider2D other) {
       if(other.CompareTag("Player")){
           player = null;
           inrange =false;
       }
   }
}
