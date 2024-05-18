using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    #region Variables
    [Tooltip("Array of canvases to be managed by this script.")]
    public Canvas[] canvases;

    [Tooltip("Text element for displaying upgrade information.")]
    public TextMeshProUGUI upgradeText;

    [Tooltip("Text element for displaying repair information.")]
    public TextMeshProUGUI repairText;

    [Tooltip("Text element for displaying upgrade cost.")]
    public TextMeshProUGUI upgradeCostText;

    [Tooltip("Text element for displaying repair cost.")]
    public TextMeshProUGUI repairCostText;

    [Tooltip("Initial BuildingData to set on wake.")]
    [SerializeField]
    private BuildingData initialBuildingData;

    private BuildingData currentBuildingData;
    #endregion

    #region Unity Methods
    void Awake()
    {
        if (initialBuildingData != null)
        {
            SetBuildingData(initialBuildingData);
            UpdateCanvasTexts(initialBuildingData);
        }
        else
        {
            Debug.LogWarning("Initial BuildingData is not set.");
        }
    }
    #endregion

    #region Public Methods
    public void SetBuildingData(BuildingData buildingData)
    {
        currentBuildingData = buildingData;
        Debug.Log("BuildingData set: " + buildingData.buildingName);
    }

    public void EnableCanvas(int index)
    {
        if (currentBuildingData == null)
        {
            Debug.LogError("BuildingData is not set.");
            return;
        }

        if (index < 0 || index >= canvases.Length)
        {
            Debug.LogError("Index out of range.");
            return;
        }

        if (canvases[index] != null)
        {
            canvases[index].enabled = true;
            canvases[index].gameObject.SetActive(true);
            UpdateCanvasTexts(currentBuildingData);
        }
        else
        {
            Debug.LogError("Canvas at index " + index + " is null.");
        }
    }

    public void DisableCanvas(int index)
    {
        if (index < 0 || index >= canvases.Length)
        {
            Debug.LogError("Index out of range.");
            return;
        }

        if (canvases[index] != null)
        {
            canvases[index].enabled = false;
            canvases[index].gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Canvas at index " + index + " is null.");
        }
    }

    public void SetDataAndEnableCanvas(BuildingData buildingData, int index)
    {
        SetBuildingData(buildingData);
        EnableCanvas(index);
        Debug.Log("Canvas enabled with index: " + index);
    }
    #endregion

    #region Private Methods
    private void UpdateCanvasTexts(BuildingData buildingData)
    {
        UpdateUpgradeText(buildingData);
        UpdateRepairText(buildingData);
        UpdateUpgradeCost(buildingData);
        UpdateRepairCost(buildingData);
    }

    private void UpdateUpgradeText(BuildingData buildingData)
    {
        if (upgradeText != null && buildingData != null)
        {
            upgradeText.text = buildingData.upgradeDescription;
        }
        else
        {
            Debug.LogError("Upgrade text or building data is null.");
        }
    }

    private void UpdateRepairText(BuildingData buildingData)
    {
        if (repairText != null && buildingData != null)
        {
            repairText.text = buildingData.repairDescription;
        }
        else
        {
            Debug.LogError("Repair text or building data is null.");
        }
    }

    private void UpdateUpgradeCost(BuildingData buildingData)
    {
        if (upgradeCostText != null && buildingData != null)
        {
            upgradeCostText.text = "Upgrade Cost: " + buildingData.upgradeCost;
        }
        else
        {
            Debug.LogError("Upgrade cost text or building data is null.");
        }
    }

    private void UpdateRepairCost(BuildingData buildingData)
    {
        if (repairCostText != null && buildingData != null)
        {
            repairCostText.text = "Repair Cost: " + buildingData.repairCost;
        }
        else
        {
            Debug.LogError("Repair cost text or building data is null.");
        }
    }
    #endregion
}
