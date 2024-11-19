using System;
using UnityEngine;
using UnityEngine.UI;

public class UISwapper : MonoBehaviour
{
    public GameObject[] oldPanels;
    public Sprite[] newSprites;

    public GameObject[] oldButtons;
    public Sprite[] newBttnSprites;

    void Start()
    {
        EventManager.Instance.TriggerUpgradeCinematicEvent.AddListener(SwapAssets);
    }

    void OnDisable()
    {
        EventManager.Instance.TriggerUpgradeCinematicEvent.RemoveListener(SwapAssets);
    }

    void SwapAssets(){
        SwapPanels();
        SwapButtons();
    }

    void SwapPanels(){
        if (oldPanels.Length != newSprites.Length)
        {
            Debug.LogError("The number of old panels and new sprites must be the same.");
            return;
        }

        for (int i = 0; i < oldPanels.Length; i++)
        {
            Image oldImage = oldPanels[i].GetComponent<Image>();

            if (oldImage != null)
            {
                oldImage.sprite = newSprites[i];
            }
            else
            {
                Debug.LogWarning("One of the old panels is missing an Image component.");
            }
        }
    }
    void SwapButtons()
    {
        if (oldButtons.Length != newBttnSprites.Length)
        {
            Debug.LogError("The number of old Buttons and new button sprites must be the same.");
            return;
        }

        for (int i = 0; i < oldButtons.Length; i++)
        {
            Image oldButtonImage = oldButtons[i].GetComponent<Image>();

            if (oldButtonImage != null)
            {
                oldButtonImage.sprite = newBttnSprites[i];
            }
            else
            {
                Debug.LogWarning("One of the old panels is missing an Image component.");
            }
        }

    }
}