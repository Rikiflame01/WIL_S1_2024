using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class GeneratorMiniGameLogic : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Image image;

    public string wireColor;
    public string socketColor;
    public GameObject redWireLitUp;
    public GameObject blueWireLitUp;
    public GameObject greenWireLitUp;

    public bool isSocket;

    private Vector2 originalPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
        isSocket = !string.IsNullOrEmpty(socketColor);
    }

    private void Start()
    {
        if (!isSocket)
        {
            originalPosition = rectTransform.anchoredPosition;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isSocket) return;

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

        if (image != null)
        {
            image.raycastTarget = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isSocket) return;

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isSocket) return;

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (image != null)
        {
            image.raycastTarget = true;
        }

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        bool socketFound = false;

        foreach (var result in results)
        {
            GeneratorMiniGameLogic socket = result.gameObject.GetComponent<GeneratorMiniGameLogic>();
            if (socket != null && socket.isSocket)
            {
                socketFound = true;
                if (socket.socketColor == wireColor)
                {
                    rectTransform.position = socket.rectTransform.position;
                    enabled = false;

                    EnableLitUpImage(wireColor);

                    FindObjectOfType<GeneratorMiniGameManager>().CheckCompletion();
                }
                else
                {
                    ResetPosition();
                }
                break;
            }
        }

        if (!socketFound)
        {
            ResetPosition();
        }
    }

    public void ResetPosition()
    {
        if (!isSocket)
        {
            rectTransform.anchoredPosition = originalPosition;

            enabled = true;

            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;

            DisableLitUpImage(wireColor);
        }
    }

    private void EnableLitUpImage(string color)
    {
        switch (color)
        {
            case "Red":
                if (redWireLitUp != null)
                {
                    redWireLitUp.SetActive(true);
                }
                break;
            case "Blue":
                if (blueWireLitUp != null)
                {
                    blueWireLitUp.SetActive(true);
                }
                break;
            case "Green":
                if (greenWireLitUp != null)
                {
                    greenWireLitUp.SetActive(true);
                }
                break;
        }
    }

    private void DisableLitUpImage(string color)
    {
        switch (color)
        {
            case "Red":
                if (redWireLitUp != null)
                {
                    redWireLitUp.SetActive(false);
                }
                break;
            case "Blue":
                if (blueWireLitUp != null)
                {
                    blueWireLitUp.SetActive(false);
                }
                break;
            case "Green":
                if (greenWireLitUp != null)
                {
                    greenWireLitUp.SetActive(false);
                }
                break;
        }
    }
}
