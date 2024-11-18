using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingLevelTracker : MonoBehaviour
{
    private int transitionCount = 0;
    public int ElectricityBuildingLevel { get; private set; } = 1;
    public int WaterBuildingLevel { get; private set; } = 1;    
    public int BankBuildingLevel { get; private set; } = 1;

    private void Start()
    {
        EventManager.Instance.UpgradeBuilding.AddListener(UpgradeBuilding);
    }

    private void OnDisable()
    {
        EventManager.Instance.UpgradeBuilding.RemoveListener(UpgradeBuilding);
    }
    public void UpgradeBuilding(string building)
    {
        switch (building)
        {
            case "Generator":
                ElectricityBuildingLevel++;
                Debug.Log("Electricity Building Level: " + ElectricityBuildingLevel);
                break;
            case "WaterPump":
                WaterBuildingLevel++;
                Debug.Log("Water Building Level: " + WaterBuildingLevel);
                break;
            case "Bank":
                BankBuildingLevel++;
                Debug.Log("Bank Building Level: " + BankBuildingLevel);
                break;
            default:
                break;
        }
        if (transitionCount == 0)
        {
            if (ElectricityBuildingLevel >= 5 && WaterBuildingLevel >= 5 
                && BankBuildingLevel >= 5)
            {
                EventManager.Instance.TriggerUpgradeCinematic();
                Debug.Log("Building Cinematic Triggered");
                transitionCount++;
            }
        }
    }
}
