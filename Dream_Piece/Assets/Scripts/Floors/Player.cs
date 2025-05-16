using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
// Coroutine
using System.Collections;
using UnityEngine.Lumin;
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
    public bool canMove = true;
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
    [SerializeField]
    Transform RespawnTransform2;
    // Below Jump Value
    [SerializeField]
    private string platformLayerName = "Platform";
    private bool isDropping = false;
    // Animation Control Value
    private PlayerAnima animaController;
    [SerializeField]
    private GameObject DeathAnimation;
    private bool isDead = false;
    //
   
    bool isBouncing = false;
    float bounceCooldown = 1f;
    bool isSaved = false;

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
        isSaved = false;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
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

            if (Input.GetKeyDown(KeyCode.Space) && grounded)
            {
                SoundManager.Instance.PlaySFX("jumpClip");
            }

            float vy = rid.linearVelocityY;
            float inputX = Input.GetAxisRaw("Horizontal");
            vx = rid.linearVelocity.x;

            // Left Right Check
            if (inputX != 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Sign(inputX) * Mathf.Abs(scale.x);
                transform.localScale = scale;
            }
            // Below Jump Check and Animation
            if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space) && grounded && !isDropping)
            {
                SoundManager.Instance.PlaySFX("jumpClip");
                animaController.PlayBelowJump();
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
            else if (isOnIce)
            {
                if (Mathf.Abs(vx) < 0.1f)
                {
                    vx += inputX * (iceAcceleration + 5f) * Time.deltaTime;
                }
                else
                {
                    vx += inputX * iceAcceleration * Time.deltaTime;
                }

                // Below Jump Check and Animation
                if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space) && grounded && !isDropping)
                {
                    animaController.PlayBelowJump();
                    StartCoroutine(DropThroughPlatform());
                }
                //
                else if (Input.GetButtonDown("Jump") && grounded)
                {
                    vy = JumpSpeed;
                    SoundManager.Instance.PlaySFX("jumpClip");
                }

                if (!isOnIce)
                {
                    vx = inputX * Speed;
                }
                else if (isOnIce)
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
            }
            rid.linearVelocity = new Vector2(vx, vy);

            // SMH bug fix
            if (Mathf.Abs(inputX) > 0.1f && grounded)
            {
                SoundManager.Instance.PlayRunLoop(grounded); // isGrounded 전달
            }
            else
            {
                SoundManager.Instance.StopRunLoop(); // 멈추면 중단
            }
            // Animation Update
            animaController.UpdateAnimation(inputX, grounded);
            //
        }
    }

    // Platform Drop Coroutine
    IEnumerator DropThroughPlatform()
    {
        isDropping = true;
        int playerLayer = gameObject.layer;
        int platformLayer = LayerMask.NameToLayer(platformLayerName);

        Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, true);

        yield return new WaitForSeconds(1.0f);

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

    public IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1f);

        if (isSaved)
        {
            gameObject.transform.position = RespawnTransform2.position;
        }
        else
        {
            gameObject.transform.position = RespawnTransform.position;
        }
        // ���� = false, �����Ӱ��� = true
        isDead = false;
        canMove = true;
        //
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

        if (collision.CompareTag("ClearPortal") && grounded)
        {
            Debug.Log("A");
            GameManager.Instance.StartStageClear();
        }

        if (collision.CompareTag("Fall") && !isDead)
        {
            // ���� = true, �����Ӱ��� = false
            isDead = true;
            canMove = false;
            // ���� �������� ������ ���� 0���� �����Ѵ�.
            rid.linearVelocity = Vector2.zero;
            if (isDead)
            Instantiate(DeathAnimation, transform.position, Quaternion.identity);
            //
            //Death.SetActive(true);
            StartCoroutine(Respawn());
            SoundManager.Instance.PlaySFX("Die");
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

        if (collision.CompareTag("SavePoint"))
        {
            isSaved = true;
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

