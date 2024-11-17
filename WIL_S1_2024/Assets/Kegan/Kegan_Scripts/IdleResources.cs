using System.Linq;
using TMPro;
using UnityEngine;
using System.Collections;

public class IdleResources : MonoBehaviour
{
    public static IdleResources Instance { get; private set; }

    // Resource Amounts and UI Texts
    public TMP_Text randAmountText;
    public TMP_Text waterAmountText;
    public TMP_Text electricityAmountText;
    public int rand = 10;
    public int water = 10;
    public int electricity = 10;

    // Idle Resource Gain
    private float timer = 0f;
    public float idleGainInterval = 3f;
    private Building buildingScript;
    private BuildingData buildingDataScript;

    
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start(){
        EventManager.Instance.UpgradeCost.AddListener(UpgradeHandling);
    }

    private void OnDisable()
    {
        EventManager.Instance.UpgradeCost.RemoveListener(UpgradeHandling);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= idleGainInterval)
        {
            timer = 0f;
            AddResource(BuildingData.ResourceType.Rand, 0);
            AddResource(BuildingData.ResourceType.Water, 0);
            AddResource(BuildingData.ResourceType.Electricity, 0);
        }
    }

    public void UpgradeHandling(string buildingName, int cost)
    {
        if (buildingName == "Generator")
        {
            if (CanAffordUpgrade(cost))
            {
                SpendResource(BuildingData.ResourceType.Rand, cost);
            }
            else
            {
                Debug.Log("Cannot upgrade. Insufficient rand.");
            }
        }
        else if (buildingName == "WaterPump")
        {
            if (CanAffordUpgrade(cost))
            {
                SpendResource(BuildingData.ResourceType.Rand, cost);
            }
            else
            {
                Debug.Log("Cannot upgrade. Insufficient rand.");
            }
        }
        else if (buildingName == "Bank")
        {
            if (CanAffordUpgrade(cost))
            {
                SpendResource(BuildingData.ResourceType.Rand, cost);
            }
            else
            {
                Debug.Log("Cannot upgrade. Insufficient rand.");
            }
        }
    }

    public void AddResource(BuildingData.ResourceType type, int amount)
    {
        Building building = FindObjectsOfType<Building>().FirstOrDefault(b => b.buildingData.producedResource == type);

        if (building != null)
        {
            if (building.buildingData.status == BuildingData.BuildingStatus.Broken)
            {
                Debug.Log(building.name + " is broken");
            }
            else
            {
                amount = building.buildingData.currentOutput;
                switch (type)
                {
                    case BuildingData.ResourceType.Rand:
                        rand += amount;
                        break;
                    case BuildingData.ResourceType.Water:
                        water += amount;
                        break;
                    case BuildingData.ResourceType.Electricity:
                        electricity += amount;
                        break;
                }
            }

        }
        else
        {
            Debug.LogWarning($"No building found for resource type: {type}");
        }

        UpdateTexts();
    }

    public bool CanAffordUpgrade(int cost)
    {
        return rand >= cost;
    }

    public void SpendResource(BuildingData.ResourceType resourceType, int amount)
    {
        switch (resourceType)
        {
            case BuildingData.ResourceType.Rand:
                rand -= amount;
                break;
                //handle other resource types if needed
        }

        UpdateTexts();
    }


    public bool CanAffordRepair(int cost)
    {
        return rand >= cost;
    }



    private void UpdateTexts()
    {
        if (randAmountText != null)
        {
            randAmountText.text = rand.ToString();
        }

        if (waterAmountText != null)
        {
            waterAmountText.text = water.ToString();
        }

        if (electricityAmountText != null)
        {
            electricityAmountText.text = electricity.ToString();
        }
    }
    public void OnRepairButtonClick()
    {
        Debug.Log("repair clcked");
        if (rand >= buildingDataScript.repairCost)
        {
            rand -= buildingDataScript.repairCost;
            buildingScript.RepairBuilding();

            UpdateTexts();
        }
        else
        {
            //Not enough gold or building already repaired
            Debug.Log("Cannot repair. Insufficient gold or building is already repaired.");
        }
    }
}



