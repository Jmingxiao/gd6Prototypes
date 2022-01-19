using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private uint score ;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("candy")){
                score++;
        }
    }
    

}
