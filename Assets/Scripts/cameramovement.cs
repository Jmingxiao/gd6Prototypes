using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameramovement : MonoBehaviour
{

     GameObject Cam;
     bool Movehere;
     float journeytime = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Cam = GameObject.FindGameObjectWithTag("MainCamera");


        if (Movehere)
        {

            Cam.transform.position = Vector3.Lerp(Cam.transform.position, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10), journeytime);


        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Player")
        {

            Movehere = true;


        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {

            Movehere = false;



        }

    }

}
