using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;
    public float rotationSpeed = 5f;

    private void LateUpdate()
    {
        if (player != null)
        {
            Vector3 desiredPosition = player.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            if (Mouse.current.rightButton.isPressed)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                float horizontalInput = Mouse.current.delta.ReadValue().x * rotationSpeed * Time.deltaTime;
                offset = Quaternion.AngleAxis(horizontalInput, Vector3.up) * offset;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            transform.LookAt(player);
        }
    }
}
