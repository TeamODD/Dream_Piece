using UnityEngine;

public class DeathDummyAutoDestroy : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1.0f);
    }
}
