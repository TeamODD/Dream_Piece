using Unity.Cinemachine;
using UnityEngine;

public class CameraFinder : MonoBehaviour
{
    public Transform target;
    public float fixedY = 5f;
    public float fixedZ = -10f; // ���� ī�޶�� Z�� -10�� ��

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
