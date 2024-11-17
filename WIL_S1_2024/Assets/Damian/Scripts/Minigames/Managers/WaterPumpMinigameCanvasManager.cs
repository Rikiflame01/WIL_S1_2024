using UnityEngine;
using UnityEngine.UI;

public class WaterPumpMinigameCanvasManager : MonoBehaviour
{

    [Tooltip("The canvas to be enabled when the button is clicked.")]
    [SerializeField] private Canvas waterPumpMinigameCanvas;

    [Tooltip("The button that will trigger the canvas to be enabled.")]
    [SerializeField] private Button triggerButton;

    private void Awake()
    {
        if (waterPumpMinigameCanvas == null)
        {
            Debug.LogError("WaterPumpMinigameCanvas is not assigned.");
            return;
        }

        if (triggerButton == null)
        {
            Debug.LogError("TriggerButton is not assigned.");
            return;
        }

        waterPumpMinigameCanvas.gameObject.SetActive(false);
        triggerButton.onClick.AddListener(OnTriggerButtonClicked);
    }

    private void OnTriggerButtonClicked()
    {
        waterPumpMinigameCanvas.gameObject.SetActive(true);
    }
}
