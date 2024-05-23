/*using UnityEngine;
using TMPro;
using System.Collections;

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
    public int productionAmount = 1;
    #endregion

    #region Unity Methods
    void Start()
    {
        buildingData.currentOutput = (int)buildingData.baseOutput; 
        StartCoroutine(ProduceResource());
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
            UpdateBuildingInfoText(); // Initialize the text with building info
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
            // Implement resource production logic here
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
    private IEnumerator ProduceResource()
    {
        while (true)
        {
            yield return new WaitForSeconds(buildingData.resourceInterval);
            IdleResources.Instance.AddResource(buildingData.producedResource, buildingData.currentOutput); // Direct static access
        }
    }

    
     public void UpgradeBuilding()
    {
        // Upgrade Conditions
        if (buildingData.status != BuildingData.BuildingStatus.Working)
        {
            Debug.LogWarning("Cannot upgrade a broken building.");
            return; // Exit if the building is broken
        }
        
        // Resource Cost Check
        if (!CanAffordUpgrade())
        {
            Debug.LogWarning("Not enough resources to upgrade!");
            return;
        }

        // Pay Upgrade Cost
        // (call a function in your resource manager to deduct the cost)

        // Upgrade Building
        buildingData.level++;
        productionAmount++;
        buildingData.currentOutput = (int)(buildingData.baseOutput + (buildingData.outputPerLevel * buildingData.level)); // Recalculate current output based on level

        // Update UI
        UpdateBuildingInfoText();
        Debug.Log(buildingData.buildingName + " upgraded to level " + buildingData.level);
    }

    private bool CanAffordUpgrade()
    {
        // Example implementation:
        return IdleResources.Instance.rand >= buildingData.upgradeCost;  
    }
    
    public void RepairBuilding()
    {
        // Repair Conditions
        if (buildingData.status != BuildingData.BuildingStatus.Broken)
        {
            Debug.LogWarning("Building is not broken.");
            return; // Exit if the building is not broken
        }

        // Resource Cost Check
        if (!CanAffordRepair())
        {
            Debug.LogWarning("Not enough resources to repair!");
            return;
        }

        // Pay Repair Cost
        // (call a function in your resource manager to deduct the cost)

        // Repair Building 
        buildingData.status = BuildingData.BuildingStatus.Working;
        StopCoroutine(ProduceResource());
        StartCoroutine(ProduceResource());

        // Update UI
        UpdateBuildingInfoText();
        Debug.Log(buildingData.buildingName + " repaired!");
    }

    private bool CanAffordRepair()
    {
        // Example implementation:
        return IdleResources.Instance.rand >= buildingData.repairCost;
    }
    #endregion
}*/
using UnityEngine;
using TMPro;
using System.Collections;

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
    public void UpgradeBuilding()
    {
        // Upgrade Conditions
        if (buildingData.status != BuildingData.BuildingStatus.Working)
        {
            Debug.LogWarning("Cannot upgrade a broken building.");
            return; // Exit if the building is broken
        }
        
        // Resource Cost Check
        if (!CanAffordUpgrade())
        {
            Debug.LogWarning("Not enough resources to upgrade!");
            return;
        }

        // Pay Upgrade Cost
        // (call a function in your resource manager to deduct the cost)

        // Upgrade Building
        buildingData.Upgrade();

        // Update UI
        UpdateBuildingInfoText();
        Debug.Log(buildingData.buildingName + " upgraded to level " + buildingData.level);
    }
    private bool CanAffordUpgrade()
    {
        // Example implementation:
        return IdleResources.Instance.rand >= buildingData.upgradeCost;  
    }
    
    public void RepairBuilding()
    {
        // Repair Conditions
        if (buildingData.status != BuildingData.BuildingStatus.Broken)
        {
            Debug.LogWarning("Building is not broken.");
            return; // Exit if the building is not broken
        }

        // Resource Cost Check
        if (!CanAffordRepair())
        {
            Debug.LogWarning("Not enough resources to repair!");
            return;
        }

        // Pay Repair Cost
        // (call a function in your resource manager to deduct the cost)

        // Repair Building 
        buildingData.status = BuildingData.BuildingStatus.Working;
        StopCoroutine(ProduceResource());
        StartCoroutine(ProduceResource());

        // Update UI
        UpdateBuildingInfoText();
        Debug.Log(buildingData.buildingName + " repaired!");
    }

    private bool CanAffordRepair()
    {
        // Example implementation:
        return IdleResources.Instance.rand >= buildingData.repairCost;
    }
    private IEnumerator ProduceResource()
    {
        while (buildingData.status == BuildingData.BuildingStatus.Working)
        {
            yield return new WaitForSeconds(buildingData.resourceInterval);
            IdleResources.Instance.AddResource(buildingData.producedResource, buildingData.GetCurrentOutput());
        }
    }
    #endregion
}
