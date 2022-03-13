using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float forcemod=5.0f;
    [SerializeField] attackcol atk;

    Vector2 dir;
    float timer;
    float force;
    Rigidbody2D rb2d;
    bool grounded;
    bool onmouse;
    int punchtimes=1;
    bool front =true;
    Animator anima;
    int health;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
        health = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(health>0){
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 playerPosition = transform.position;
            dir = (worldPosition - playerPosition).normalized;

            mouseUpdate();
            if(onmouse){
                timer+=Time.deltaTime*2;
            }
        }
        if(health==0){
            health--;
            StartCoroutine(death());
        }
    }
    private void FixedUpdate() {
        if(Physics2D.CircleCast(transform.position,0.1f,Vector2.down, 0.3f, LayerMask.GetMask("Ground"))){
            punchtimes =1;
            grounded =true;
        }else{
            grounded =false;
        }
    }
    void mouseUpdate(){
        if(Input.GetMouseButtonDown(0)&&punchtimes>0){
            onmouse =true;
        }
        if(Input.GetMouseButtonUp(0)&&punchtimes>0){
            onmouse =false;
            force = Mathf.Clamp(timer,1.5f,forcemod);
            if(front!=dir.x>=0){
                front = !front;
                transform.Rotate(Vector3.up*180);
            }
            rb2d.AddForce(dir*force*forcemod,ForceMode2D.Impulse);
            timer = 1;
            punchtimes--;
            anima.SetTrigger("punch");
            atk.attack();
        }
    }
    public void GotHarm(){
        health--;
        anima.SetTrigger("hurt");
    }
    IEnumerator death(){
        anima.SetTrigger("dead");
        anima.SetBool("death",true);
        yield return new WaitForSeconds(3.0f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

    }
}
