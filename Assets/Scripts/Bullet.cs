using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]float speed; 
    // Start is called before the first frame update
    [HideInInspector]public Vector3 dir;

    // Update is called once per frame
    void Update()
    {
       transform.position+= dir*Time.deltaTime*speed;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            //Todo:
            gameObject.SetActive(false);
        }else if(!other.CompareTag("Enemy")){
            gameObject.SetActive(false);
        }
        
    }
}
