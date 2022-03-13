using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackcol : MonoBehaviour
{
    Enemy enemy;
    bool attacked = false;
    float interval =0.0f;
    public void attack(){
        Debug.Log(0);
        attacked =true;
        interval = 1.0f;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")&&attacked){
            other.GetComponent<Enemy>().GotHarm();
            Debug.Log(1);
            attacked =false;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        
        if(other.CompareTag("Enemy")&&attacked){
            other.GetComponent<Enemy>().GotHarm();
            Debug.Log(1);
            attacked =false;
        }
    }
}
