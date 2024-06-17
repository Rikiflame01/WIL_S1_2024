using UnityEngine;

public class PlayerObstructionHandler : MonoBehaviour
{

    [Tooltip("Layer for objects that can obstruct the view.")]
    public LayerMask obstructionLayer;

    private Collider collider;

    private void Start()
    {
        collider = GetComponent<Collider>();
        if (collider == null)
        {
            Debug.LogError("No Collider attached to the game object. Please add a collider.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsInObstructionLayer(other.gameObject))
        {
            Renderer renderer = other.GetComponent<Renderer>();
            if (renderer != null)
            {
                Debug.Log($"Disabling renderer of {other.gameObject.name}");
                renderer.enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsInObstructionLayer(other.gameObject))
        {
            Renderer renderer = other.GetComponent<Renderer>();
            if (renderer != null)
            {
                Debug.Log($"Enabling renderer of {other.gameObject.name}");
                renderer.enabled = true;
            }
        }
    }

    private bool IsInObstructionLayer(GameObject obj)
    {
        return ((1 << obj.layer) & obstructionLayer.value) != 0;
    }
}
