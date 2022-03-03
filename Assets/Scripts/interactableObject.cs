using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactableObject : MonoBehaviour
{
    Transform player;
    [SerializeField]AudioSource soundeffect;
    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void Update() {
        if(Input.GetKeyDown(KeyCode.E)&&checkInrange()){
            interact();
            PlayeScript.dread =false;
        }
    }
    protected virtual void interact(){
        soundeffect.Play();
    }
    protected virtual bool checkInrange(){
        var dist = (player.position-transform.position).magnitude;
        if(dist<0.5f){
            return true;
        }
        return false;
    }
}
