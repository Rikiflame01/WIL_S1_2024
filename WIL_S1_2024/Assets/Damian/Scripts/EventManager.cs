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
    public UnityEvent<string> UpgradeBulding = new UnityEvent<string>();


    private void Start()
    {
        if (TriggerUpgradeCinematicEvent == null)
        {
            TriggerUpgradeCinematicEvent = new UnityEvent();
        }
        if (UpgradeBulding == null)
        {
            UpgradeBulding = new UnityEvent<string>();
        }
    }

    public void TriggerUpgradeCinematic()
    {
        TriggerUpgradeCinematicEvent?.Invoke();
    }

    public void TriggerUpgradeBuilding(string building)
    {
        Debug.Log("Upgrade Building Event Triggered" + building);
        UpgradeBulding?.Invoke(building);
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