using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;

    private void LateUpdate()
    {
        SmoothFollow();
    }

    public void SmoothFollow()
    {
        Vector3 targetPos = target.position + offset;
        Vector3 smoothFollow = Vector3.Lerp(transform.position, targetPos, smoothSpeed);

        transform.position = smoothFollow;
    }
}
