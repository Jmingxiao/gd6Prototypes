using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nursescript : MonoBehaviour
{
    [SerializeField]Transform player;
    [SerializeField]GameObject bubble;
    [SerializeField] Sprite sp;
    SpriteRenderer sr;
    Vector3 startplace;


    static bool told =false;
    // Start is called before the first frame update
    void Start()
    {
        startplace = transform.position;
        if(!told){
         StartCoroutine(Told(player.position));
        }
        bubble.SetActive(false);
        sr = GetComponent<SpriteRenderer>();


    }
        // Update is called once per frame
    IEnumerator Told(Vector3 tar){
        var dist = (transform.position-tar).magnitude;
        while(dist>1.0f){
            transform.position = Vector3.MoveTowards(transform.position,tar,Time.deltaTime*2.0f);
             dist = (transform.position-tar).magnitude;
            yield return null;
        }
        bubble.SetActive(true);
        yield return new WaitForSeconds(2); 
        bubble.SetActive(false);
        sr.sprite = sp;
        told =true;
        PlayeScript.getnotice =true;
    }
    private void Update() {
        if(told){
            move(startplace);
        }
    }
    void move(Vector3 tar){
        var dist = (transform.position-tar).magnitude;
        if(dist>0.5f){
            transform.position = Vector3.MoveTowards(transform.position,tar,Time.deltaTime*2.0f);
        }
    }

}
