using System;
using UnityEngine;

public class BuildingEventManager : MonoBehaviour
{
    public static event Action<BuildingData> OnBuildingSelected;

    public static void SelectBuilding(BuildingData buildingData)
    {
        OnBuildingSelected?.Invoke(buildingData);
    }
}
