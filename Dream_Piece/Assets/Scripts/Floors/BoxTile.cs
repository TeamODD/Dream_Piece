using UnityEngine;

public class BoxTile : MonoBehaviour
{
    public float BoxDissapearTime = 2f;
    public float BoxAppearTime = 5f;
    private bool isTriggered = false;


    private void OnCollisionEnter2D(Collision2D Collision)
    {
        if (Collision.gameObject.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
           Invoke("Dissapear", BoxDissapearTime);
        }
    }

    private void Dissapear()
    {
        gameObject.SetActive(false);
        Invoke("Respwan", BoxAppearTime);
    }
    private void Respwan()
    {
        gameObject.SetActive(true);
        isTriggered = false;
    }
}
