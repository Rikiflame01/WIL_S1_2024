using UnityEngine;

public class TradingShopManager : MonoBehaviour
{
    [Tooltip("Reference to the IdleResources script.")]
    public IdleResources idleResources;

    //Trades 20 Rand for 20 Water and 20 Electricity
    public void TradeRandForResources()
    {
        if (idleResources.rand >= 20)
        {
            idleResources.SpendResource(BuildingData.ResourceType.Rand, 20);
            idleResources.buyResource(BuildingData.ResourceType.Water, 20);
            idleResources.buyResource(BuildingData.ResourceType.Electricity, 20);
            Debug.Log("Traded 20 Rand for 20 Water and 20 Electricity.");
        }
        else
        {
            Debug.Log("Not enough Rand to trade.");
        }
    }

    //Trades 20 Electricity and 20 Water for 40 Rand
    public void TradeResourcesForRand()
    {
        if (idleResources.electricity >= 20 && idleResources.water >= 20)
        {
            idleResources.SpendResource(BuildingData.ResourceType.Electricity, 20);
            idleResources.SpendResource(BuildingData.ResourceType.Water, 20);
            idleResources.buyResource(BuildingData.ResourceType.Rand, 40);
            Debug.Log("Traded 20 Electricity and 20 Water for 40 Rand.");
        }
        else
        {
            Debug.Log("Not enough resources to trade for Rand.");
        }
    }
}