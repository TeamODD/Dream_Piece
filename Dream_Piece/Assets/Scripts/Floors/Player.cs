using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
// Coroutine
using System.Collections;
//

public class Player : MonoBehaviour
{
    public float Speed = 8;
    private float currentSpeed;
    public float JumpSpeed = 2f;
    public float PushForce = 10f;
    //public float BounceForce = 2f;
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
    [SerializeField]
    Transform RespawnTransform;
    // Below Jump Value
    [SerializeField]
    private string platformLayerName = "Platform";
    private bool isDropping = false;
    // Animation Control Value
    private PlayerAnima animaController;
    //

    bool isBouncing = false;
    float bounceCooldown = 1f;

    float vx = 0;
    Rigidbody2D rid;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.DreamPiece = 0;
        rid = GetComponent<Rigidbody2D>();
        currentSpeed = Speed;

        // Animation Control Value
        animaController = GetComponent<PlayerAnima>();
        //
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

        // Below Jump Check
        if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space) && grounded && !isDropping)
        {
            StartCoroutine(DropThroughPlatform());
        }
        //
        else if (Input.GetButtonDown("Jump") && grounded)
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

        // Animation Update
        animaController.UpdateAnimation(inputX, grounded);
        //
    }

    // Platform Drop Coroutine
    IEnumerator DropThroughPlatform()
    {
        isDropping = true;
        int playerLayer = gameObject.layer;
        int platformLayer = LayerMask.NameToLayer(platformLayerName);

        Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, true);

        yield return new WaitForSeconds(0.5f);

        Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, false);
        isDropping = false;
    }
    //

    void BounceInDirection(Vector2 dir)
    {
        if (isBouncing) return;

        rid.linearVelocity = Vector2.zero;
        rid.AddForce(dir * bounceForce, ForceMode2D.Impulse);

        // Bounce Animation
        animaController.PlayBounce();
        //

        isBouncing = true;
        bounceCooldown = 1f;
    }

    public void Respawn()
    {
       gameObject.transform.position = RespawnTransform.position;
    }

    public void ReStart()
    {
        Time.timeScale = 1f;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            grounded = true;
        }

        if (collision.CompareTag("DreamPiece"))
        {
            GameManager.Instance.AddDreamPiece();
        }

        if (collision.CompareTag("ClearPortal"))
        {
            GameManager.Instance.StageClear();
        }

        if (collision.CompareTag("Fall"))
        {
            Respawn();
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

