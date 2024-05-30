using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    public GameObject interactionCanvasPrefab;
    public Transform canvasSpawnPoint;
    private GameObject instantiatedCanvas;
    public Camera mainCamera;

    [Tooltip("Building data to select when the player enters this trigger.")]
    public BuildingData buildingData;

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
            if (instantiatedCanvas == null && canvasSpawnPoint != null)
            {
                instantiatedCanvas = Instantiate(interactionCanvasPrefab, canvasSpawnPoint.position, Quaternion.identity);
                if (buildingData != null)
                {
                    BuildingEventManager.SelectBuilding(buildingData);
                    Debug.Log("Building selected: " + buildingData.buildingName);
                }
                else
                {
                    Debug.LogError("BuildingData is not assigned to the InteractionTrigger.");
                }
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
