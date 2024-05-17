using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingData buildingData;
    public Canvas buildingCanvas;
    public Camera mainCamera;

    private bool isPlayerNearby;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (buildingCanvas != null)
        {
            buildingCanvas.enabled = false;
        }
    }

    void Update()
    {
        if (buildingCanvas != null && buildingCanvas.enabled)
        {
            buildingCanvas.transform.LookAt(mainCamera.transform);
            buildingCanvas.transform.Rotate(0, 180, 0); // Adjust if the canvas is facing the wrong way
        }

        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            ToggleCanvas();
        }

        if (buildingData.status == BuildingData.BuildingStatus.Working)
        {
            // Implement resource production logic here if necessary
        }
    }

    private void ToggleCanvas()
    {
        Debug.Log("Toggling canvas");

        if (buildingCanvas != null)
        {
            buildingCanvas.enabled = !buildingCanvas.enabled;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (buildingCanvas != null)
            {
                buildingCanvas.enabled = false;
            }
        }
    }
}
