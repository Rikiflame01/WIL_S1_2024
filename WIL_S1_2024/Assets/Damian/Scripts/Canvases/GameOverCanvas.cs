using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameOverCanvas : MonoBehaviour
{
    public GameObject gameOverCanvas;

    void Start()
    {
        EventManager.Instance.TriggerUpgradeCinematicEvent.AddListener(ShowGameOverCanvas);
    }

    void OnDisable()
    {
        EventManager.Instance.TriggerUpgradeCinematicEvent.RemoveListener(ShowGameOverCanvas);
    }

    private void ShowGameOverCanvas()
    {
        StartCoroutine(StartEndCanvas());
    }

    private IEnumerator StartEndCanvas(){

        yield return new WaitForSeconds(10f);
        gameOverCanvas.SetActive(true);
        yield return new WaitForSeconds(10f);
        gameOverCanvas.SetActive(false);
    }
    
    public void ExitCanvas()
    {
        gameOverCanvas.SetActive(false);
    }
}
