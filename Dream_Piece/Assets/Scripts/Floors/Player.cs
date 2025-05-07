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
    [SerializeField]
    private float bounceForce = 8f;

    bool isBouncing = false;
    float bounceCooldown = 0.5f;

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
        if (isBouncing)
        {
            bounceCooldown -= Time.deltaTime;
            if (bounceCooldown <= 0)
            {
                isBouncing = false;
            }
            return;
        }


        float vy = rid.linearVelocityY;
        float inputX = Input.GetAxisRaw("Horizontal");
        vx = rid.linearVelocity.x;

        if (Input.GetButtonDown("Jump") && grounded)
        {
            vy = JumpSpeed;
        }

        if (!isOnIce)
        {
            vx = inputX * Speed;
        }
        else if(isOnIce)
        {
            if (Mathf.Abs(vx) < 0.1f)
            {
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

    void BounceInDirection(Vector2 dir)
    {
        if (isBouncing) return;

        rid.linearVelocity = Vector2.zero;
        rid.AddForce(dir * bounceForce, ForceMode2D.Impulse);

        isBouncing = true;
        bounceCooldown = 0.5f;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            grounded = true;
        }

        if (collision.CompareTag("BRB"))
        {
            BounceInDirection(new Vector2(1, -1).normalized); 
        }
        else if (collision.CompareTag("BLT"))
        {
            BounceInDirection(new Vector2(-1, 1).normalized); 
        }
        else if (collision.CompareTag("BRT"))
        {
            BounceInDirection(new Vector2(1, 1).normalized); 
        }
        else if (collision.CompareTag("BLB"))
        {
            BounceInDirection(new Vector2(-1, -1).normalized); 
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

