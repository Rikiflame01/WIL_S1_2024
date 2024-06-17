using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaterPumpMiniGameActivator : MonoBehaviour
{
    [Tooltip("The UI Text component for the countdown message.")]
    [SerializeField] private TMP_Text countdownText;
    [Tooltip("The canvas to enable after the countdown.")]
    [SerializeField] private Canvas miniGameCanvas;
    [Tooltip("The panel to enable when the countdown starts.")]
    [SerializeField] private GameObject countdownPanel;
    [Tooltip("The duration of the countdown in seconds.")]
    [SerializeField] private float countdownDuration = 10f;

    private float countdownTimer;
    private bool isPlayerInTrigger;

    void Start()
    {
        if (countdownText == null)
        {
            Debug.LogError("Countdown Text is not assigned!");
            return;
        }

        if (miniGameCanvas == null)
        {
            Debug.LogError("MiniGame Canvas is not assigned!");
            return;
        }

        if (countdownPanel == null)
        {
            Debug.LogError("Countdown Panel is not assigned!");
            return;
        }

        miniGameCanvas.gameObject.SetActive(false);
        countdownText.gameObject.SetActive(false);
        countdownPanel.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInTrigger)
        {
            countdownTimer -= Time.deltaTime;
            countdownText.text = $"Stay here to start the waterpipe minigame!\n\nCurrent rewards: +100 water.\n\nTime remaining: {countdownTimer:F1} seconds";

            if (countdownTimer <= 0)
            {
                miniGameCanvas.gameObject.SetActive(true);
                countdownText.gameObject.SetActive(false);
                countdownPanel.SetActive(false);
                isPlayerInTrigger = false; 
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            countdownTimer = countdownDuration;
            countdownText.gameObject.SetActive(true);
            countdownPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            countdownText.gameObject.SetActive(false);
            countdownPanel.SetActive(false);
        }
    }
}
