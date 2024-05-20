using UnityEngine;
using TMPro;

/// <summary>
/// Manages building interactions, including displaying information and toggling canvas visibility.
/// </summary>
public class Building : MonoBehaviour
{

    #region Variables
    [Tooltip("Data for the building.")]
    public BuildingData buildingData;

    [Tooltip("Canvas displaying building information.")]
    public Canvas buildingCanvas;

    [Tooltip("Main camera in the scene.")]
    public Camera mainCamera;

    [Tooltip("Text element for displaying building information.")]
    public TextMeshProUGUI buildingInfoText;

    private bool isPlayerNearby;
    #endregion

    #region Unity Methods
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

        if (buildingInfoText != null)
        {
            UpdateBuildingInfoText();
        }
    }

    void Update()
    {
        if (buildingCanvas != null && buildingCanvas.enabled)
        {
            buildingCanvas.transform.LookAt(mainCamera.transform);
            buildingCanvas.transform.Rotate(0, 180, 0);
        }

        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            ToggleCanvas();
        }

        if (buildingData != null && buildingData.status == BuildingData.BuildingStatus.Working)
        {
            // Implement resource production logic here?
        }
    }
    #endregion

    #region Private Methods
    private void ToggleCanvas()
    {
        if (buildingCanvas != null)
        {
            buildingCanvas.enabled = !buildingCanvas.enabled;

            if (buildingCanvas.enabled && buildingInfoText != null)
            {
                UpdateBuildingInfoText();
            }
        }
    }

    private void UpdateBuildingInfoText()
    {
        if (buildingInfoText != null && buildingData != null)
        {
            buildingInfoText.text = buildingData.GetBuildingInfo();
        }
    }
    #endregion

    #region Trigger Methods
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
    #endregion
}
