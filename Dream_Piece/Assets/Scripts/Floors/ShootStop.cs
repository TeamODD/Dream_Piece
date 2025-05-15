using System.Collections;
using UnityEngine;

public class ShootStop : MonoBehaviour
{
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void StopAfterSeconds(float seconds)
    {
        StartCoroutine(StopCoroutine(seconds));
    }

    private IEnumerator StopCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        rb.linearVelocity = Vector2.zero;
    }
}
