using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class candy : MonoBehaviour
{
    float dropRate;
    Vector3 startPos;
    private void Start() {
        startPos = transform.position;
        dropRate = Random.Range(Config.MinDropRate,Config.MaxDropRate);
    }
    float timer =0.0f;
     private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            destroycandy();
        }else if(other.CompareTag("Ground")){
            PlayerPowerUp.LossLife();
            destroycandy();
        }
    }

    private void FixedUpdate() {
        timer+= Time.deltaTime;
        Drop();
        
    }
    void Drop(){
        if(timer>dropRate){
            timer=0.0f;
            transform.position += Config.NodeSize*Vector3.down;
        }
    }
    void destroycandy(){
        transform.position = startPos;
        gameObject.SetActive(false);
    }
}
