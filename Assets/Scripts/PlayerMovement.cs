using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float forcemod=5.0f;
    Vector2 dir;
    float timer;
    float force;
    Rigidbody2D rb2d;
    bool grounded;
    bool onmouse;
    int punchtimes=1;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPosition = transform.position;
        dir = (worldPosition - playerPosition).normalized;

        mouseUpdate();
        if(onmouse){
            timer+=Time.deltaTime*2;
        }
    }
    private void FixedUpdate() {
        if(Physics2D.CircleCast(transform.position,0.1f,Vector2.down, 0.5f, LayerMask.GetMask("Ground"))){
            Debug.Log(1);
            punchtimes =1;
        }
    }
    void mouseUpdate(){
        if(Input.GetMouseButtonDown(0)&&punchtimes>0){
            onmouse =true;
        }
        if(Input.GetMouseButtonUp(0)&&punchtimes>0){
            onmouse =false;
            force = Mathf.Clamp(timer,1.5f,forcemod);
            rb2d.AddForce(dir*force*forcemod,ForceMode2D.Impulse);
            timer = 1;
            punchtimes--;
        }
    }
}
