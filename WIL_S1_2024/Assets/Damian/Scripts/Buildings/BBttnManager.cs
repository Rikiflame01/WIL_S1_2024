using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBttnManager : MonoBehaviour
{
    public Building bank;
    public Building generator;
    public Building waterPump;

    private Building selectedBuilding;

    // Method to set the currently selected building
    public void SelectBuilding(string buildingName)
    {
        switch (buildingName)
        {
            case "Bank":
                selectedBuilding = bank;
                break;
            case "Generator":
                selectedBuilding = generator;
                break;
            case "WaterPump":
                selectedBuilding = waterPump;
                break;
            default:
                Debug.LogWarning("Invalid building name selected.");
                selectedBuilding = null;
                break;
        }

        if (selectedBuilding != null)
        {
            Debug.Log(buildingName + " selected.");
        }
    }

    // Method to upgrade the currently selected building
    public void UpgradeSelectedBuilding()
    {
        if (selectedBuilding != null)
        {
            selectedBuilding.UpgradeBuilding();
            Debug.Log("Yes was selected for upgrade prompt.");
        }
        else
        {
            Debug.LogWarning("No building selected for upgrade.");
        }
    }
}