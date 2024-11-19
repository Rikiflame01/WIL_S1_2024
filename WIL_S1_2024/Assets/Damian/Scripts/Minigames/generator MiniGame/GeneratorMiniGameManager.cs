using UnityEngine;

public class GeneratorMiniGameManager : MonoBehaviour
{
    [Tooltip("Wires for the mini-game.")]
    public GameObject[] wires;

    [Tooltip("Sockets for the mini-game.")]
    public GameObject[] sockets;

    [Tooltip("Panel to display upon completion.")]
    public GameObject completionPanel;

    private void OnEnable()
    {
        //Reset game state whenever the canvas is enabled
        ResetGame();
    }

    private void ResetGame()
    {
        completionPanel.SetActive(false);

        foreach (GameObject wire in wires)
        {
            var wireConnector = wire.GetComponent<GeneratorMiniGameLogic>();
            wireConnector.enabled = true;

            //Reset position to original state
            wire.GetComponent<RectTransform>().anchoredPosition = wireConnector.GetComponent<RectTransform>().anchoredPosition;
        }
    }

    public void CheckCompletion()
    {
        foreach (GameObject wire in wires)
        {
            if (wire.GetComponent<GeneratorMiniGameLogic>().enabled)
            {
                return; //Not all wires are connected yet
            }
        }

        //All wires connected
        completionPanel.SetActive(true);
        Debug.Log("Mini-game completed!");
    }
}