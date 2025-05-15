using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    GameObject Enemy;
    [SerializeField]
    Player player;
    private SpriteRenderer sprite;
    public float fadeDuration = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = Enemy.GetComponent<SpriteRenderer>();
        Color c = sprite.color;
        c.a = 0f;
        Enemy.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(EnemySpawnCoroutine());
        }
    }

    IEnumerator EnemySpawnCoroutine()
    {
        player.canMove = false;
        Enemy.SetActive(true);

        float elapsed = 0f;
        Color c = sprite.color;

        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            sprite.color = new Color(c.r, c.g, c.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 최종적으로 완전히 보이게
        sprite.color = new Color(c.r, c.g, c.b, 1f);
        player.canMove = true;
    }
}
