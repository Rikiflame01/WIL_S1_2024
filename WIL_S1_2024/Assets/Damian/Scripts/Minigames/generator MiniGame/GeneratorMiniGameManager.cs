using System.Collections;
using UnityEngine;

public class GeneratorMiniGameManager : MonoBehaviour
{
    [Tooltip("Wires for the mini-game.")]
    public GameObject[] wires;

    [Tooltip("Sockets for the mini-game.")]
    public GameObject[] sockets;

    [Tooltip("Panel to display upon completion.")]
    public GameObject completionPanel;

    [Tooltip("Main generator canvas that will be deactivated after completion.")]
    public Canvas generatorMiniGameCanvas; 

    private void OnEnable()
    {
        ResetGame();
    }

    private void ResetGame()
    {
        completionPanel.SetActive(false); //Hide the completion panel initially

        foreach (GameObject wire in wires)
        {
            var wireConnector = wire.GetComponent<GeneratorMiniGameLogic>();
            wireConnector.enabled = true; 
            
            wire.GetComponent<RectTransform>().anchoredPosition = wireConnector.GetComponent<RectTransform>().anchoredPosition;
        }
    }

    public void CheckCompletion()
    {
        //Check if all wires are connected
        foreach (GameObject wire in wires)
        {
            if (wire.GetComponent<GeneratorMiniGameLogic>().enabled)
            {
                return; //Not all wires are connected yet
            }
        }

        //All wires are connected, show the completion panel
        completionPanel.SetActive(true);
        Debug.Log("Mini-game completed!");

        //Add 100 electricity upon completion
        IdleResources.Instance.AddResource(BuildingData.ResourceType.Electricity, 100);

        
        StartCoroutine(HideCanvasAfterDelay());
    }

    private IEnumerator HideCanvasAfterDelay()
    {
        yield return new WaitForSeconds(3f);

        //Hide the entire generator canvas
        if (generatorMiniGameCanvas != null)
        {
            generatorMiniGameCanvas.gameObject.SetActive(false);
            Debug.Log("Generator Mini-Game canvas hidden.");
        }
        else
        {
            Debug.LogError("GeneratorMiniGameCanvas is not assigned!");
        }

        Debug.Log("Generator Mini-Game completed and entire canvas hidden.");
    }
}
