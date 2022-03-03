using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nursescript : MonoBehaviour
{
    [SerializeField]PlayeScript player;
    [SerializeField]GameObject bubble;
    [SerializeField] Sprite sp;
    Sprite sp1;
    SpriteRenderer sr;
    Vector3 startplace;
    AudioSource audioSource;


    static bool told =false;
    // Start is called before the first frame update
    void Start()
    {
        startplace = transform.position;
        if(!told){
         StartCoroutine(Told(player.transform));
        }
        bubble.SetActive(false);
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        sp1 = sr.sprite;

    }
        // Update is called once per frame
    IEnumerator Told(Transform tar){
        var dist = (transform.position-tar.position).magnitude;
        while(dist>1.0f){
            transform.position = Vector3.MoveTowards(transform.position,tar.position,Time.deltaTime*2.0f);
             dist = (transform.position-tar.position).magnitude;
            yield return null;
        }
        bubble.SetActive(true);
        player.bubble.SetActive(true);
        yield return new WaitForSeconds(2); 
        bubble.SetActive(false);
        player.bubble.SetActive(false);
        sr.sprite = sp;
        told =true;
        PlayeScript.getnotice =true;
    }
    private void Update() {
        if(told){
            move(startplace);
        }
        var dist = (transform.position-player.transform.position).magnitude;
        if(dist<1.0f&&Input.GetKeyDown(KeyCode.E)){
            StartCoroutine(talk());
        }
    }
    IEnumerator talk(){
        sr.sprite = transform.position.x-player.transform.position.x>0?sp:sp1;
        PlayeScript.dread =false;
        bubble.SetActive(true);
        audioSource.Play();
        yield return new WaitForSeconds(1); 
        bubble.SetActive(false);
    }



    void move(Vector3 tar){
        var dist = (transform.position-tar).magnitude;
        if(dist>0.5f){
            transform.position = Vector3.MoveTowards(transform.position,tar,Time.deltaTime*2.0f);
        }
    }

}
