using System.Collections;
using UnityEngine;

public class NormalTile : MonoBehaviour
{
    public GameObject Eff;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Eff.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(CloudCor());
        }
    }

    IEnumerator CloudCor()
    {
        Eff.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        Eff.SetActive(false);
    }
}
