using UnityEngine;

public class IceTile : MonoBehaviour
{
    
    public Player player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.isOnIce = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           
            player.isOnIce = false;
        }
    }

}

