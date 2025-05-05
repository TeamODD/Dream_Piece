using UnityEngine;

public class IceTile : MonoBehaviour
{
    [SerializeField] public float IncreaseSpeed = 1f;
    public float IceSlip = 0.1f;
    public float IceDrag = 1f;
    Player player;

    private void OnCollisionEnter2D(Collision2D Collision)
    {
        if (Collision.gameObject.CompareTag("Player"))
        {
            Player player = Collision.gameObject.GetComponent<Player>();
            Rigidbody2D playerRb = Collision.gameObject.GetComponent<Rigidbody2D>();
            player.Speed += IncreaseSpeed;
            Vector2 slideDirection = playerRb.linearVelocity.normalized;

            playerRb.linearVelocity += slideDirection * IceSlip * Time.fixedDeltaTime;
            playerRb.linearDamping = IceDrag;

        }
    }

    private void OnCollisionExit2D(Collision2D Collision)
    {

        if (Collision.gameObject.CompareTag("Player"))
        {
            Player player = Collision.gameObject.GetComponent<Player>();
             Rigidbody2D playerRb = Collision.gameObject.GetComponent<Rigidbody2D>();
            player.Speed -= IncreaseSpeed;
            playerRb.linearDamping = 0;

    }
}
}
