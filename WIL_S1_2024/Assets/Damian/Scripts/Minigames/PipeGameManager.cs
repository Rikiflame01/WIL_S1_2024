using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PipeGameManager : MonoBehaviour
{
    [Tooltip("Button representing the start of the pipe route.")]
    public Button startButton;

    [Tooltip("Button representing the end of the pipe route.")]
    public Button endButton;

    [Tooltip("Array of buttons representing the pipes.")]
    public Button[] pipeButtons;

    [Tooltip("Panel that displays the completion message.")]
    public GameObject messagePanel;

    [Tooltip("Duration for the smooth rotation of the pipes.")]
    public float rotationDuration = 0.5f;

    private int[] pipeStates;
    private int totalPipes;
    private Dictionary<int, Coroutine> activeCoroutines = new Dictionary<int, Coroutine>();
    private Dictionary<int, Quaternion> targetRotations = new Dictionary<int, Quaternion>();
    private Canvas parentCanvas;

    void Start()
    {
        InitializeGame();
        parentCanvas = GetComponentInParent<Canvas>();
    }

    private void InitializeGame()
    {
        totalPipes = pipeButtons.Length;
        pipeStates = new int[totalPipes];

        for (int i = 0; i < totalPipes; i++)
        {
            int index = i;
            pipeButtons[i].onClick.AddListener(() => RotatePipe(index));
            targetRotations[i] = pipeButtons[i].transform.rotation;
        }

        messagePanel.SetActive(false);
        GenerateMap();
    }

    private void RotatePipe(int index)
    {
        pipeStates[index] = (pipeStates[index] + 1) % 4;
        targetRotations[index] *= Quaternion.Euler(0, 0, 90);

        if (activeCoroutines.ContainsKey(index))
        {
            StopCoroutine(activeCoroutines[index]);
        }

        activeCoroutines[index] = StartCoroutine(SmoothRotate(pipeButtons[index].transform, targetRotations[index]));
        CheckPipesAlignment();
    }

    private IEnumerator SmoothRotate(Transform target, Quaternion targetRotation)
    {
        Quaternion startRotation = target.rotation;
        float elapsedTime = 0;

        while (elapsedTime < rotationDuration)
        {
            target.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        target.rotation = targetRotation;
    }

    private void GenerateMap()
    {
        RectTransform startRect = startButton.GetComponent<RectTransform>();
        RectTransform endRect = endButton.GetComponent<RectTransform>();
        RectTransform panelRect = GetComponent<RectTransform>();

        float panelWidth = panelRect.rect.width;
        float buttonWidth = pipeButtons[0].GetComponent<RectTransform>().rect.width;

        float leftEdge = -panelWidth / 2 + buttonWidth / 2;
        float rightEdge = panelWidth / 2 - buttonWidth / 2;
        float spacing = (rightEdge - leftEdge) / (totalPipes + 1);

        PositionButton(startRect, leftEdge, "Start Button");
        PositionButton(endRect, rightEdge, "End Button");

        for (int i = 0; i < totalPipes; i++)
        {
            RectTransform pipeRect = pipeButtons[i].GetComponent<RectTransform>();
            float xPos = leftEdge + (i + 1) * spacing;
            pipeRect.anchoredPosition = new Vector2(xPos, 0);
            Debug.Log($"Pipe Button {i} Position: {pipeRect.anchoredPosition}");

            pipeStates[i] = Random.Range(1, 4);
            pipeButtons[i].transform.rotation = Quaternion.Euler(0, 0, 90 * pipeStates[i]);
            targetRotations[i] = pipeButtons[i].transform.rotation;
        }
    }

    private void PositionButton(RectTransform rect, float position, string buttonName)
    {
        rect.anchoredPosition = new Vector2(position, 0);
        Debug.Log($"{buttonName} Position: {rect.anchoredPosition}");
    }

    private void CheckPipesAlignment()
    {
        foreach (int state in pipeStates)
        {
            if (state != 0)
            {
                return;
            }
        }
        messagePanel.SetActive(true);
        StartCoroutine(ShowMessagePanel());
    }

    private IEnumerator ShowMessagePanel()
    {
        yield return new WaitForSeconds(5f);
        messagePanel.SetActive(false);
        parentCanvas.gameObject.SetActive(false);
    }
}
