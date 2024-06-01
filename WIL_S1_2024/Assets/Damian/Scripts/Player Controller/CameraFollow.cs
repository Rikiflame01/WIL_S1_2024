using UnityEngine;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour
{
    [Tooltip("The player transform to follow.")]
    public Transform player;

    [Tooltip("Offset of the camera relative to the player.")]
    public Vector3 offset;

    [Tooltip("Smoothness factor for camera movement.")]
    public float smoothSpeed = 0.125f;

    [Tooltip("Layer for objects that can obstruct the view.")]
    public LayerMask obstructionLayer;

    private List<Renderer> obstructedRenderers = new List<Renderer>();

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

    private void OnTriggerEnter(Collider other)
    {
        if (IsInObstructionLayer(other.gameObject))
        {
            Renderer renderer = other.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
                obstructedRenderers.Add(renderer);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsInObstructionLayer(other.gameObject))
        {
            Renderer renderer = other.GetComponent<Renderer>();
            if (renderer != null && obstructedRenderers.Contains(renderer))
            {
                renderer.enabled = true;
                obstructedRenderers.Remove(renderer);
            }
        }
    }

    private bool IsInObstructionLayer(GameObject obj)
    {
        return ((1 << obj.layer) & obstructionLayer.value) != 0;
    }
}
