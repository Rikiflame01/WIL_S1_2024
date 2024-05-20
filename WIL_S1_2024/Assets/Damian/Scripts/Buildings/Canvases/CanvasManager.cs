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
        BuildingEventManager.OnBuildingSelected += SetBuildingData;

        AssignTextMeshProReferences();

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

    void OnDestroy()
    {
        BuildingEventManager.OnBuildingSelected -= SetBuildingData;
    }
    #endregion

    #region Public Methods
    public void SetBuildingData(BuildingData buildingData)
    {
        currentBuildingData = buildingData;
        Debug.Log("BuildingData set: " + buildingData.buildingName);
        UpdateCanvasTexts(buildingData);
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
            canvases[index].gameObject.SetActive(true); // Ensure the canvas is active
            canvases[index].GetComponent<CanvasGroup>().alpha = 1; // Ensure the canvas is fully visible
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
            canvases[index].gameObject.SetActive(false); // Ensure the canvas is inactive
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
    private void AssignTextMeshProReferences()
    {
        Canvas repairCanvas = FindCanvasWithTag("RepairConfirmationCanvas");
        if (repairCanvas != null)
        {
            repairText = FindTextMeshProInChildren(repairCanvas, "RepairText");
            repairCostText = FindTextMeshProInChildren(repairCanvas, "RepairCostText");
        }

        Canvas upgradeCanvas = FindCanvasWithTag("UpgradeConfirmationCanvas");
        if (upgradeCanvas != null)
        {
            upgradeText = FindTextMeshProInChildren(upgradeCanvas, "UpgradeText");
            upgradeCostText = FindTextMeshProInChildren(upgradeCanvas, "UpgradeCostText");
        }
    }

    private Canvas FindCanvasWithTag(string tag)
    {
        GameObject canvasObject = GameObject.FindWithTag(tag);
        if (canvasObject != null)
        {
            return canvasObject.GetComponent<Canvas>();
        }
        else
        {
            Debug.LogError("Canvas with tag " + tag + " not found.");
            return null;
        }
    }

    private TextMeshProUGUI FindTextMeshProInChildren(Canvas canvas, string textObjectName)
    {
        Transform textTransform = canvas.transform.Find(textObjectName);
        if (textTransform != null)
        {
            return textTransform.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogError("TextMeshProUGUI with name " + textObjectName + " not found in canvas " + canvas.name);
            return null;
        }
    }

    private void UpdateCanvasTexts(BuildingData buildingData)
    {
        if (buildingData == null)
        {
            Debug.LogError("Building data is null.");
            return;
        }

        UpdateUpgradeText(buildingData);
        UpdateRepairText(buildingData);
        UpdateUpgradeCost(buildingData);
        UpdateRepairCost(buildingData);
    }

    private void UpdateUpgradeText(BuildingData buildingData)
    {
        if (upgradeText != null)
        {
            upgradeText.text = buildingData.upgradeDescription;
        }
        else
        {
            Debug.LogError("Upgrade text is null.");
        }
    }

    private void UpdateRepairText(BuildingData buildingData)
    {
        if (repairText != null)
        {
            repairText.text = buildingData.repairDescription;
        }
        else
        {
            Debug.LogError("Repair text is null.");
        }
    }

    private void UpdateUpgradeCost(BuildingData buildingData)
    {
        if (upgradeCostText != null)
        {
            upgradeCostText.text = "Upgrade Cost: " + buildingData.upgradeCost;
        }
        else
        {
            Debug.LogError("Upgrade cost text is null.");
        }
    }

    private void UpdateRepairCost(BuildingData buildingData)
    {
        if (repairCostText != null)
        {
            repairCostText.text = "Repair Cost: " + buildingData.repairCost;
        }
        else
        {
            Debug.LogError("Repair cost text is null.");
        }
    }
    #endregion
}
