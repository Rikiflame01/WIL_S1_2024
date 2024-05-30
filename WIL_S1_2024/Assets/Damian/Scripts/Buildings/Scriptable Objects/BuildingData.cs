using UnityEngine;

[CreateAssetMenu(fileName = "NewBuildingData", menuName = "Building Data", order = 51)]
public class BuildingData : ScriptableObject
{
    public string buildingName;
    public BuildingStatus status;
    public float baseOutput; //ase output when built
    public float outputPerLevel = 1; //Additional output per upgrade
    public int currentOutput; 
    public int level;
    public string resourceProduced;
    public float resourceInterval;
    public int repairCost;

    public int initialUpgradeCost = 20;  
    public int upgradeCostIncrease = 50;

    public string upgradeDescription;
    public string repairDescription;
    
    public enum ResourceType { Rand, Water, Electricity }
    public ResourceType producedResource;
    public Building buildingScript;
    public int idleGainAmount = 1;
    

    public string GetBuildingInfo()
    {
        return "Building Name: " + buildingName + "\n" +
               "Status: " + status + "\n" +
               "Level: " + level + "\n" +
               "Resource Produced: " + resourceProduced + "\n" +
               "Resource Interval: " + resourceInterval + " seconds";
    }

    public enum BuildingStatus
    {
        Broken,
        Working
    }
    public void Upgrade()
    {
        level++;
        idleGainAmount++;
        currentOutput = (int)(baseOutput + (outputPerLevel * level));
    }

    public int GetCurrentOutput()
    {
        throw new System.NotImplementedException();
    }
}
