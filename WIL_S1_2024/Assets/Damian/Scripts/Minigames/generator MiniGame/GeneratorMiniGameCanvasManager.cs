using UnityEngine;
using UnityEngine.UI;

public class GeneratorMiniGameCanvasManager : MonoBehaviour
{
    [Tooltip("The canvas for the generator mini-game.")]
    [SerializeField] private Canvas generatorMiniGameCanvas;

    [Tooltip("The button that activates the generator mini-game.")]
    [SerializeField] private Button triggerButton;

    private void Awake()
    {
        if (generatorMiniGameCanvas == null)
        {
            Debug.LogError("GeneratorMiniGameCanvas is not assigned.");
            return;
        }

        if (triggerButton == null)
        {
            Debug.LogError("TriggerButton is not assigned.");
            return;
        }

        generatorMiniGameCanvas.gameObject.SetActive(false);
        triggerButton.onClick.AddListener(ActivateMiniGameCanvas);
    }

    private void ActivateMiniGameCanvas()
    {
        generatorMiniGameCanvas.gameObject.SetActive(true);
    }
}