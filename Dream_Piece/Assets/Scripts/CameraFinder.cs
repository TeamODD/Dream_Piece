using Unity.Cinemachine;
using UnityEngine;

public class CameraFinder : MonoBehaviour
{
    public CinemachineCamera CinCamera;
    public Transform target;
    public float yThreshold = -5f;

    private bool isFollowing = true;

    void Update()
    {
        if (target.position.y < yThreshold && isFollowing)
        {
            CinCamera.Follow = null;
            isFollowing = false;
        }
        else if (target.position.y >= yThreshold && !isFollowing)
        {
            CinCamera.Follow = target;
            isFollowing = true;
        }
    }
}
