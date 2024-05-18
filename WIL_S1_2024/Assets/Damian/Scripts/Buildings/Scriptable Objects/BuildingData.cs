using UnityEngine;

[CreateAssetMenu(fileName = "NewBuildingData", menuName = "Building Data", order = 51)]
public class BuildingData : ScriptableObject
{
    public string buildingName;
    public BuildingStatus status;
    public int level;
    public string resourceProduced;
    public float resourceInterval;
    public int repairCost;
    public int upgradeCost;
    public string upgradeDescription;
    public string repairDescription;

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
}
