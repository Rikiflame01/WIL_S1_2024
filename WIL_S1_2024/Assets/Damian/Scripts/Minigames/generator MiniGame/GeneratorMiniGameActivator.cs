using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GeneratorMiniGameActivator : MonoBehaviour
{
    [Tooltip("The UI Text component for the countdown message.")]
    [SerializeField] private TMP_Text countdownText;

    [Tooltip("The canvas to enable after the countdown.")]
    [SerializeField] private Canvas generatorMiniGameCanvas;

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

        if (generatorMiniGameCanvas == null)
        {
            Debug.LogError("Generator MiniGame Canvas is not assigned!");
            return;
        }

        if (countdownPanel == null)
        {
            Debug.LogError("Countdown Panel is not assigned!");
            return;
        }
        
        generatorMiniGameCanvas.gameObject.SetActive(false);
        countdownText.gameObject.SetActive(false);
        countdownPanel.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInTrigger)
        {
            countdownTimer -= Time.deltaTime;
            countdownText.text = $"Stay here to start the generator minigame!\n\nRewards: Fix generator, +100 energy.\n\nTime remaining: {countdownTimer:F1} seconds";

            if (countdownTimer <= 0)
            {
                generatorMiniGameCanvas.gameObject.SetActive(true); //Enables the mini-game canvas
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
            countdownTimer = countdownDuration; //Reset timer
            countdownText.gameObject.SetActive(true);
            countdownPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false; //Stop countdown
            countdownText.gameObject.SetActive(false);
            countdownPanel.SetActive(false);
        }
    }
}
