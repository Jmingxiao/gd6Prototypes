using UnityEngine;

public class CharacterController2d : MonoBehaviour
{
    [SerializeField]
    float speed = 5.0f;
    float distanceOffset =1.5f;
    bool playerjump;
    LayerMask ground ;
    float move;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ground = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        move = Input.GetAxis("Horizontal");
        playerjump |= Input.GetKeyDown(KeyCode.Space);
    }

    void FixedUpdate() {
        Jump();
        rb.velocity=new Vector2(move*speed, rb.velocity.y); 

    }
    void Jump(){
        bool grounded = Physics2D.Raycast(transform.position,Vector2.down,distanceOffset,ground);
        if(grounded&&playerjump){
            rb.AddForce(Vector2.up*speed,ForceMode2D.Impulse);
        }
        playerjump =false;
    }
}
