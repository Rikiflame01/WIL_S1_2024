using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PipeGameManager : MonoBehaviour
{
    public Button startButton;
    public Button endButton;
    public Button[] pipeButtons;
    public GameObject messagePanel;

    private int[] pipeStates;
    private int totalPipes;

    void Start()
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

    void RotatePipe(int index)
    {
        pipeStates[index] = (pipeStates[index] + 1) % 4;
        pipeButtons[index].transform.Rotate(0, 0, 90);
        CheckPipesAlignment();
    }

    void GenerateMap()
    {
        // Position Start and End Buttons
        RectTransform startRect = startButton.GetComponent<RectTransform>();
        RectTransform endRect = endButton.GetComponent<RectTransform>();
        RectTransform panelRect = GetComponent<RectTransform>();

        float panelWidth = panelRect.rect.width;
        float buttonWidth = pipeButtons[0].GetComponent<RectTransform>().rect.width;

        float leftEdge = -panelWidth / 2 + buttonWidth / 2;
        float rightEdge = panelWidth / 2 - buttonWidth / 2;
        float spacing = (rightEdge - leftEdge) / (totalPipes + 1);

        Debug.Log("Panel Width: " + panelWidth);
        Debug.Log("Button Width: " + buttonWidth);
        Debug.Log("Spacing: " + spacing);

        // Position the Start and End buttons
        startRect.anchoredPosition = new Vector2(leftEdge, 0);
        endRect.anchoredPosition = new Vector2(rightEdge, 0);

        Debug.Log("Start Button Position: " + startRect.anchoredPosition);
        Debug.Log("End Button Position: " + endRect.anchoredPosition);

        // Position Pipe Buttons between Start and End
        for (int i = 0; i < totalPipes; i++)
        {
            RectTransform pipeRect = pipeButtons[i].GetComponent<RectTransform>();
            float xPos = leftEdge + (i + 1) * spacing;
            pipeRect.anchoredPosition = new Vector2(xPos, 0);

            Debug.Log("Pipe Button " + i + " Position: " + pipeRect.anchoredPosition);
        }

        // Randomize initial pipe states to ensure they are not aligned
        for (int i = 0; i < totalPipes; i++)
        {
            pipeStates[i] = Random.Range(1, 4); // Ensure initial state is not aligned
            pipeButtons[i].transform.rotation = Quaternion.Euler(0, 0, 90 * pipeStates[i]);
        }
    }

    void CheckPipesAlignment()
    {
        foreach (int state in pipeStates)
        {
            if (state != 0) // Assuming 0 is the aligned state
            {
                return;
            }
        }
        messagePanel.SetActive(true);
    }
}
