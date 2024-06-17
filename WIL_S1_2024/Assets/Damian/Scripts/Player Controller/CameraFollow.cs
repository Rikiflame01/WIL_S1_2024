using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Tooltip("The player transform to follow.")]
    public Transform player;

    [Tooltip("Offset of the camera relative to the player.")]
    public Vector3 offset;

    [Tooltip("Smoothness factor for camera movement.")]
    public float smoothSpeed = 0.125f;

    private void LateUpdate()
    {
        if (player != null)
        {
            Vector3 desiredPosition = player.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(player);
        }
    }
}
