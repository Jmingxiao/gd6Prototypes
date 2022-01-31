using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PlayerPowerUp : MonoBehaviour
{

    private static int score = 0;
    private static int life = 3;

    AudioSource audiosource;
    [SerializeField] AudioClip moveSound;
    [SerializeField] AudioClip catchSound;
    [SerializeField] AudioClip speedSound;

    [SerializeField] Text scoreText;

    bool onGround =true;
    bool powerUpMovement;
    public int powerUpAmount = 0;
    public int powerUpTakeAway = 3;
    public int powerUpMax = 9;

     bool onLadder;
     bool leftdege;
    bool rightedge;

    float dropRate = 0.5F;
    float timer = 0.0F;

    RaycastHit2D hit;

    const float power =2.0f; 

    private void Start()
    {
        transform.position = new Vector3(.0f, -Config.SCRHeight+1, .0f);
        audiosource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("candy"))
        {
            score++;
            CandySpawner.AccelarateSpawnRate();
            audiosource.clip = catchSound;
            audiosource.Play();
           if (powerUpAmount < powerUpMax)
            {
                powerUpAmount += 1;
            }
        }

        if(other.CompareTag("ladder")){
            onLadder=true;
        }
        if(other.CompareTag("left")){
            leftdege = true;
        }
        if(other.CompareTag("right")){
            rightedge =true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("ladder")){
            onLadder=false;
        }
        if(other.CompareTag("left")){
            leftdege = false;
        }
        if(other.CompareTag("right")){
            rightedge =false;
        }
    }
    private void Update()
    {
        TextDisplay();
        if (Move())
        {
            audiosource.clip = moveSound;
            audiosource.Play();
        }
    }
    private void FixedUpdate() {
        onGround = Physics2D.Raycast(transform.position,Vector3.down,1.0f,LayerMask.GetMask("Ground"));
    }
    void Drop()
    {
        if (timer > dropRate)
        {
            timer = 0.0f;
            transform.position += Config.NodeSize * Vector3.down;
        }
    }
    public static void LossLife()
    {
        if (life > 0)
        {
            life--;
        }
        else
        {
            Debug.Log(1);
            CandySpawner.spawning = false;
        }
    }


    bool Move()
    {
        var ismoving = false;
        var powering = Input.GetKey(KeyCode.A) && powerUpAmount >= 3;
        var onedge = rightedge||leftdege;
        if (Input.GetKeyDown(KeyCode.UpArrow)&&onLadder)
        {
            transform.position += Vector3.up;
            ismoving = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)&&(onLadder||onedge)&&!onGround)
        {
            transform.position += Vector3.down;
            ismoving = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x > -Config.SCRWidth&&(onGround||rightedge))
        {
                transform.position += powering? Vector3.left*power:Vector3.left;
                if(powering){
                    powerUpAmount -= powerUpTakeAway;
                }
                ismoving = true;
            }

        if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < Config.SCRWidth&&(onGround||leftdege))
        {
            transform.position += powering? Vector3.right*power:Vector3.right;
            if(powering){
                powerUpAmount -= powerUpTakeAway;
            }
            ismoving = true;
        }
        return ismoving;
    }
    void TextDisplay()
    {
            scoreText.text = life>0?"powe up: " + powerUpAmount.ToString() + " sore: " + score.ToString() + "  life: " + life.ToString():
            "You Loss!!!" + " Score: " + score.ToString();
    }

}
