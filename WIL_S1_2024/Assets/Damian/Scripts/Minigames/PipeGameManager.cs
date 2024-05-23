using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

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

    void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        totalPipes = pipeButtons.Length;
        pipeStates = new int[totalPipes];

        for (int i = 0; i < totalPipes; i++)
        {
            int index = i;
            pipeButtons[i].onClick.AddListener(() => RotatePipe(index));
        }

        messagePanel.SetActive(false);
        GenerateMap();
    }

    private void RotatePipe(int index)
    {
        pipeStates[index] = (pipeStates[index] + 1) % 4;
        StartCoroutine(SmoothRotate(pipeButtons[index].transform, 90));
        CheckPipesAlignment();
    }

    private IEnumerator SmoothRotate(Transform target, float angle)
    {
        Quaternion startRotation = target.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, angle);
        float elapsedTime = 0;

        while (elapsedTime < rotationDuration)
        {
            target.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        target.rotation = endRotation;
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
    }
}
