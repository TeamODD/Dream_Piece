using UnityEngine;
using UnityEngine.U2D;

public class TopInv : MonoBehaviour
{
    [SerializeField] private SpriteRenderer coverSprite;
    private Color originalColor;

    private void Start()
    {
        
        originalColor = coverSprite.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TileTopFade();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TileTopFadeOut();
        }
    }

    void TileTopFade()
    {
        coverSprite.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.7f);
    }

    void TileTopFadeOut()
    {
        coverSprite.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
    }

}
