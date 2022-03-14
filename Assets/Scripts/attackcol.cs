using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackcol : MonoBehaviour
{
    Enemy enemy;
    bool attacked = false;
    Rythmcontrol rythmcontrol;

    private void Start() {
         rythmcontrol = GameObject.FindGameObjectWithTag("beat").GetComponent<Rythmcontrol>();
    }
    public void attack(){
        Debug.Log(0);
        attacked =true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")&&attacked){
            other.GetComponent<Enemy>().GotHarm();
            if(rythmcontrol.low){
            other.GetComponent<Enemy>().GotHarm();
            }
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
