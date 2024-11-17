using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    private void Awake()
    {
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



    private void Start()
    {
        if (TriggerUpgradeCinematicEvent == null)
        {
            TriggerUpgradeCinematicEvent = new UnityEvent();
        }
    }

    public void TriggerUpgradeCinematic()
    {
        TriggerUpgradeCinematicEvent?.Invoke();
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