using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class punch : MonoBehaviour
{

    public SpringJoint2D sj2d;
    public Vector3 worldPosition;
    public Vector3 playerPosition;
    public Vector3 dir;
    public float angle;
    public float time;
    public float forcemod;
    public float forcemod2;
    public  Rigidbody2D rb2d;
    public Quaternion rot;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        playerPosition = transform.position;
        dir = (worldPosition - playerPosition).normalized;
        //dir = other.transform.InverseTransformDirection(dir);
        angle = angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rot = Quaternion.Euler(dir);

        Vector3 dir2 = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;


        Quaternion rotation = Quaternion.Euler(dir);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (forcemod <= 4)
            {
                time += (Time.deltaTime *2);
                forcemod= Mathf.Ceil(time);


            }

            if (forcemod >= 5)
            {
                forcemod = 4;

            }
           


            


        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {

            //rb2d.AddForce(dir * forcemod * forcemod2);
            time = 0;
            rb2d.AddForce(dir2 * forcemod * forcemod2);
           



        }


    }
}
