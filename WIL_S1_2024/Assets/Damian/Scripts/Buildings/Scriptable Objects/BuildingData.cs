using UnityEngine;

[CreateAssetMenu(fileName = "NewBuildingData", menuName = "Building Data", order = 51)]
public class BuildingData : ScriptableObject
{
    public string buildingName;
    public BuildingStatus status;
    public int level;
    public string resourceProduced;
    public float resourceInterval;

    public enum BuildingStatus
    {
        Broken,
        Working
    }
}
