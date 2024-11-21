using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    private void Awake()
    {
        Debug.Log("EventManager Awake");
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public UnityEvent TriggerUpgradeCinematicEvent;
    public UnityEvent TriggerCinematicEvent;

    public UnityEvent<string> UpgradeBuilding = new UnityEvent<string>();
    public UnityEvent<string, int> UpgradeCost = new UnityEvent<string, int>();

    public UnityEvent TriggerGeneratorMiniGameReset = new UnityEvent();

    private void Start()
    {
        if (TriggerUpgradeCinematicEvent == null)
        {
            TriggerUpgradeCinematicEvent = new UnityEvent();
        }
        if (UpgradeBuilding == null)
        {
            UpgradeBuilding = new UnityEvent<string>();
        }
        if (UpgradeCost == null)
        {
            UpgradeCost = new UnityEvent<string, int>();
        }
        if (TriggerGeneratorMiniGameReset == null)
        {
            TriggerGeneratorMiniGameReset = new UnityEvent();
        }
        if (TriggerCinematicEvent == null)
        {
            TriggerCinematicEvent = new UnityEvent();
        }
    }

    public void TriggerCinematic()
    {
        TriggerCinematicEvent?.Invoke();
    }

    public void TriggerGeneratorMiniGameResetEvent()
    {
        TriggerGeneratorMiniGameReset?.Invoke();
    }

    public void TriggerUpgradeCinematic()
    {
        TriggerUpgradeCinematicEvent?.Invoke();
    }

    public void TriggerUpgradeBuilding(string building)
    {
        Debug.Log("Upgrade Building Event Triggered" + building);
        UpgradeBuilding?.Invoke(building);
    }

    public void TriggerUpgradeBuilding(string buildingName, int cost)
    {
        Debug.Log("Upgrade Building Event Triggered " + name+ " " + cost);
        UpgradeCost?.Invoke(buildingName, cost);
    }
}

/* How to use:
void OnEnable()
{
    EventManager.Instance.TriggerUpgradeCinematicEvent.AddListener(OnUpgradeCinematic);
}

void OnDisable()
{
    EventManager.Instance.TriggerUpgradeCinematicEvent.RemoveListener(OnUpgradeCinematic);
}

void OnUpgradeCinematic()
{
    // Handle the event
}
*/