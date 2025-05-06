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
    private bool grounded = false;
    bool isJumping = false;
    private bool canMove = true;
    public bool isOnIce = false;

    [SerializeField] 
    float iceAcceleration = 10f;
    [SerializeField] 
    float iceDeceleration = 5f;
    [SerializeField] 
    float maxIceSpeed = 5f;
    [SerializeField] 
    float iceStartBoost = 3f;

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
        float vy = rid.linearVelocityY;
        float inputX = Input.GetAxisRaw("Horizontal");
        float vx = rid.linearVelocity.x;

        if (Input.GetButtonDown("Jump") && grounded)
        {
            vy = JumpSpeed;
        }

        if (!isOnIce)
        {
            vx = inputX * Speed;
        }
        else
        {
            if (Mathf.Abs(vx) < 0.1f)
            {
                // 정지 상태에서 방향키 누를 때만 살짝 보정해서 더 세게 가속
                vx += inputX * (iceAcceleration + 5f) * Time.deltaTime;
            }
            else
            {
                vx += inputX * iceAcceleration * Time.deltaTime;
            }

            vx = Mathf.Clamp(vx, -maxIceSpeed, maxIceSpeed);
        }

        rid.linearVelocity = new Vector2(vx, vy);
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

