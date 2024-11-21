using System.Collections;
using TMPro;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    [Tooltip("Parent canvas for the intro.")]
    public GameObject IntroCanvas;

    public CanvasGroup[] canvasGroup;

    [Tooltip("Text objects to be displayed in order.")]
    public TextMeshProUGUI[] IntroTexts;

    [Tooltip("Typing speed for the text.")]
    public float typingSpeed = 0.05f;

    [Tooltip("Delay between texts.")]
    public float delayBetweenTexts = 2f;

    private string[] originalTexts;

    void Start()
    {
        foreach (CanvasGroup group in canvasGroup)
        {
            group.alpha = 0f;
        }
        
        EventManager.Instance.TriggerCinematic();
        StoreOriginalTexts();
        StartCoroutine(ShowIntro());
    }

    private void StoreOriginalTexts()
    {
        originalTexts = new string[IntroTexts.Length];
        for (int i = 0; i < IntroTexts.Length; i++)
        {
            originalTexts[i] = IntroTexts[i].text;
            IntroTexts[i].text = ""; 
            IntroTexts[i].gameObject.SetActive(false); 
        }
    }

    private IEnumerator ShowIntro()
    {
        for (int i = 0; i < IntroTexts.Length; i++)
        {
            if (i > 0)
            {
                IntroTexts[i - 1].gameObject.SetActive(false);
            }

            IntroTexts[i].gameObject.SetActive(true);

            yield return StartCoroutine(TypeText(IntroTexts[i], originalTexts[i]));

            yield return new WaitForSeconds(delayBetweenTexts);
        }

        if (IntroTexts.Length > 0)
        {
            IntroTexts[IntroTexts.Length - 1].gameObject.SetActive(false);
        }

        foreach (CanvasGroup group in canvasGroup)
        {
            group.alpha = 1f;
        }
        IntroCanvas.SetActive(false);
    }

    private IEnumerator TypeText(TextMeshProUGUI textElement, string fullText)
    {
        textElement.text = "";

        foreach (char letter in fullText)
        {
            textElement.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
