using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    public GameObject interactionCanvasPrefab;
    public Transform canvasSpawnPoint;
    private GameObject instantiatedCanvas;
    public Camera mainCamera;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        if (instantiatedCanvas != null)
        {
            instantiatedCanvas.transform.LookAt(mainCamera.transform);
            instantiatedCanvas.transform.Rotate(0, 180, 0); 
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            if 
                (instantiatedCanvas == null && canvasSpawnPoint != null)
            {
                instantiatedCanvas = Instantiate(interactionCanvasPrefab, canvasSpawnPoint.position, Quaternion.identity);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (instantiatedCanvas != null)
            {
                Destroy(instantiatedCanvas);
            }
        }
    }
}
