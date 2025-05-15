using UnityEngine;

public class PlayerAnima : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rid;

    // Check Grounded
    private bool wasGroundedLastFrame = true;

    // Get Componenet
    void Awake()
    {
        animator = GetComponent<Animator>();
        rid = GetComponent<Rigidbody2D>();
    }

    // Animation Update
    public void UpdateAnimation(float inputX, bool grounded)
    {
        bool isWalking = Mathf.Abs(inputX) > 0.1f && grounded;
        bool isJumping = !grounded && rid.linearVelocity.y > 0.1f;
        bool isMoving = Mathf.Abs(inputX) > 0.1f;

        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isGrounded", grounded);
        animator.SetBool("isJumping", isJumping);
        animator.SetFloat("yVelocity", rid.linearVelocity.y);
        animator.SetBool("isMoving", isMoving);
    }

    // Bounce Check
    public void PlayBounce()
    {
        animator.SetTrigger("isBounced");
    }

    public void PlayDeath()
    {
        animator.SetTrigger("isDead");
    }
}
