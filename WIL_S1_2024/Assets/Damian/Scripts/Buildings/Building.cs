using System.Collections;
using TMPro;
using UnityEngine;

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

    [Header("Break Down Stuff")]
    [SerializeField] private float premiumFixTime = 120f;
    //[SerializeField] private float regularFixTime;
    [SerializeField] private float minTimeTillBreakDown = 30;
    [SerializeField] private float maxTimeTillBreakDown = 60;

    #endregion

    #region Unity Methods
    void Start()
    {
        StartCoroutine(RepairBuildingCoroutine());
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
        //Upgrade Conditions
        if (buildingData.status != BuildingData.BuildingStatus.Working)
        {
            Debug.LogWarning("Cannot upgrade a broken building.");
            return; //Exit if the building is broken
        }

        //Resource Cost Check
        if (!CanAffordUpgrade())
        {
            Debug.LogWarning("Not enough resources to upgrade!");
            return;
        }

        if (CanAffordUpgrade())
        {
            //Upgrade Building
            buildingData.Upgrade();
            UpdateBuildingInfoText();
            Debug.Log(buildingData.buildingName + " upgraded to level " + buildingData.level);
            //call a function to deduct cost
        }



    }
    private bool CanAffordUpgrade()
    {
        return IdleResources.Instance.rand >= buildingData.initialUpgradeCost;
    }


    private IEnumerator RepairBuildingCoroutine()
    {

        buildingData.status = BuildingData.BuildingStatus.Working;
        Debug.Log("Building is now working.");
        float totalRepairTime;

        float randomTime = Random.Range(minTimeTillBreakDown, maxTimeTillBreakDown);

        totalRepairTime = premiumFixTime + randomTime;





        yield return new WaitForSeconds(totalRepairTime);


        buildingData.status = BuildingData.BuildingStatus.Broken;
        Debug.Log("Building has broken again after repair time elapsed.");
    }

    public void RepairBuilding()
    {

        if (buildingData.status != BuildingData.BuildingStatus.Broken)
        {
            Debug.LogWarning("Building is not broken.");
            return; // Exit if the building is not broken
        }


        if (!CanAffordRepair())
        {
            Debug.LogWarning("Not enough resources to repair!");
            return;
        }

        if (CanAffordRepair())
        {
            //call a function to deduct cost

            //Repair Building 
            //buildingData.status = BuildingData.BuildingStatus.Working;
            StartCoroutine(RepairBuildingCoroutine());
            StopCoroutine(ProduceResource());
            StartCoroutine(ProduceResource());


            UpdateBuildingInfoText();
            Debug.Log(buildingData.buildingName + " repaired!");
        }

    }

    private bool CanAffordRepair()
    {
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
