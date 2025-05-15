using Unity.Cinemachine;
using UnityEngine;

public class CameraFinder : MonoBehaviour
{
    public Transform target;
    public float fixedY = 5f;
    public float fixedZ = -10f; // 보통 카메라는 Z를 -10에 둠

    void LateUpdate()
    {
        if (target == null) return;

        transform.position = new Vector3(
            target.position.x,
            fixedY,
            fixedZ
        );
    }
}
