using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat : MonoBehaviour
{
    SpriteRenderer spr;
    private void Start() {
        spr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){
            spr.enabled=false;
        }
    }
}
