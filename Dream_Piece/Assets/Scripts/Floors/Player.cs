using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 8;
    private float currentSpeed;
    public float JumpSpeed = 2f;
    public float PushForce = 10f;
    //public float BounceForce = 2f;
    public float SlowSpeed = 3f;
    public float SlowJump = 3f;
    private bool grounded = true;
    bool isJumping = false;
    private bool canMove = true;
    float vx = 0;
    Rigidbody2D rid;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rid = GetComponent<Rigidbody2D>();
        currentSpeed = Speed;
    }

    // Update is called once per frame
    void Update()
    {
        vx = Input.GetAxisRaw("Horizontal") * Speed;
        float vy = GetComponent<Rigidbody2D>().linearVelocityY;

        if (Input.GetButtonDown("Jump") && grounded)
        {
            vy = JumpSpeed;
        }
      

        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(vx, vy);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            grounded = false;
        }
    }
}


