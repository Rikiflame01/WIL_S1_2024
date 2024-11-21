using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PipeGameManager : MonoBehaviour
{
    [Tooltip("Parent canvas for the game.")]
    public Canvas parentCanvas;

    [Tooltip("Array of buttons representing the pipes.")]
    public Button[] pipeButtons;

    [Tooltip("Panel that displays the completion message.")]
    public GameObject messagePanel;

    private int rotationLimit = 10;
    private HashSet<int> rotatedIndexes;
    private float[] originalZRotations;

    void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        originalZRotations = new float[pipeButtons.Length];

        for (int i = 0; i < pipeButtons.Length; i++)
        {
            int index = i;
            originalZRotations[i] = Mathf.Round(pipeButtons[i].transform.localEulerAngles.z);
            pipeButtons[i].onClick.AddListener(() => RotatePipe(index));
        }

        messagePanel.SetActive(false);

        ApplyRandomRotations();
    }

    private void RotatePipe(int index)
    {
        pipeButtons[index].transform.Rotate(0, 0, 90);

        CheckPipesAlignment();
    }

    private void ApplyRandomRotations()
    {
        rotatedIndexes = new HashSet<int>();
        while (rotatedIndexes.Count < rotationLimit && rotatedIndexes.Count < pipeButtons.Length)
        {
            int randomIndex = Random.Range(0, pipeButtons.Length);
            rotatedIndexes.Add(randomIndex);
        }

        foreach (int index in rotatedIndexes)
        {
            int randomRotationSteps = Random.Range(1, 4);
            pipeButtons[index].transform.Rotate(0, 0, 90 * randomRotationSteps);

            Vector3 currentEulerAngles = pipeButtons[index].transform.localEulerAngles;
            currentEulerAngles.z = Mathf.Round(NormalizeAngle(currentEulerAngles.z));
            pipeButtons[index].transform.localEulerAngles = currentEulerAngles;
        }
    }

    private void CheckPipesAlignment()
    {
        int correctCount = 0;

        for (int i = 0; i < pipeButtons.Length; i++)
        {
            float currentZ = Mathf.Round(NormalizeAngle(pipeButtons[i].transform.localEulerAngles.z));
            float originalZ = Mathf.Round(NormalizeAngle(originalZRotations[i]));

            if (Mathf.Approximately(currentZ, originalZ))
            {
                correctCount++;
            }
        }

        if (correctCount == pipeButtons.Length)
        {
            StartCoroutine(ShowMessagePanel());
        }
    }

    private IEnumerator ShowMessagePanel()
    {
        messagePanel.SetActive(true);
        IdleResources.Instance.water += 100;
        yield return new WaitForSeconds(5f);
        messagePanel.SetActive(false);
        ApplyRandomRotations();
        parentCanvas.gameObject.SetActive(false);
    }

    private float NormalizeAngle(float angle)
    {
        while (angle < 0) angle += 360;
        while (angle >= 360) angle -= 360;
        return angle;
    }
}
