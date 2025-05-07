using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class BoxTile : MonoBehaviour
{
    public float BoxDissapearTime = 2f;
    public float BoxAppearTime = 3f;
    private bool isTriggered = false;
    private Color[] originalColor;

    private SpriteRenderer[] sr;

    private void Start()
    {
        sr = GetComponentsInChildren<SpriteRenderer>();

        // 각각의 원래 색 저장
        originalColor = new Color[sr.Length];
        for (int i = 0; i < sr.Length; i++)
        {
            originalColor[i] = sr[i].color;
        }
    }
    private void OnCollisionEnter2D(Collision2D Collision)
    {
        if (Collision.gameObject.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            StartCoroutine(Dissapear());
        }
    }

    private IEnumerator Dissapear()
    {
        float time = 0f;

        while (time < BoxDissapearTime)
        {
            time += Time.deltaTime;
            float ratio = Mathf.Lerp(1f, 0f, time / BoxDissapearTime);

            for (int i = 0; i < sr.Length; i++)
            {
                var c = originalColor[i];
                float adjustedAlpha = c.a * ratio;
                sr[i].color = new Color(c.r, c.g, c.b, adjustedAlpha);
            }

            yield return null;
        }

        for (int i = 0; i < sr.Length; i++)
        {
            var c = originalColor[i];
            sr[i].color = new Color(c.r, c.g, c.b, 0f);
            sr[i].enabled = false;
        }

        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(BoxAppearTime);

        Respawn();
    }

    private void Respawn()
    {
        for (int i = 0; i < sr.Length; i++)
        {
            sr[i].enabled = true;
            sr[i].color = originalColor[i];
        }

        GetComponent<Collider2D>().enabled = true;
        isTriggered = false;
    }
}
