using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTutorial : MonoBehaviour
{
    public GameObject tutorialCanvas;

    void Start()
    {
        StartCoroutine(DisableTutorialCanvas());
    }

    IEnumerator DisableTutorialCanvas()
    {
        yield return new WaitForSeconds(20);
        tutorialCanvas.SetActive(false);
    }
}