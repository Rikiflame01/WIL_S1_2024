using UnityEngine;
using UnityEngine.EventSystems;

public class GeneratorMiniGameLogic : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    [Tooltip("The wire's color. Must match the socket's color.")]
    public string wireColor;

    [Tooltip("The socket's color if this GameObject is a socket.")]
    public string socketColor;

    [Tooltip("Reference to the lit-up wire images.")]
    public GameObject redWireLitUp;
    public GameObject blueWireLitUp;
    public GameObject greenWireLitUp;

    private bool isSocket;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        isSocket = !string.IsNullOrEmpty(socketColor);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isSocket) return;

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
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
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!isSocket) return;

        GeneratorMiniGameLogic wire = eventData.pointerDrag.GetComponent<GeneratorMiniGameLogic>();
        if (wire != null && wire.wireColor == socketColor)
        {
            Debug.Log($"Correct wire connected! Color: {wire.wireColor}");
            wire.transform.position = transform.position; //Snap the wire to the socket
            wire.enabled = false; //Disable further dragging

            //Enable the corresponding LitUp image
            EnableLitUpImage(wire.wireColor);

            FindObjectOfType<GeneratorMiniGameManager>().CheckCompletion();
        }
        else if (wire != null)
        {
            Debug.Log("Incorrect wire!");
        }
    }

    private void EnableLitUpImage(string color)
    {
        Debug.Log($"Enabling LitUp image for color: {color}");
        switch (color)
        {
            case "Red":
                if (redWireLitUp != null)
                {
                    redWireLitUp.SetActive(true);
                    Debug.Log("Red lit-up image enabled.");
                }
                else
                {
                    Debug.LogWarning("RedWireLitUp is not assigned!");
                }
                break;
            case "Blue":
                if (blueWireLitUp != null)
                {
                    blueWireLitUp.SetActive(true);
                    Debug.Log("Blue lit-up image enabled.");
                }
                else
                {
                    Debug.LogWarning("BlueWireLitUp is not assigned!");
                }
                break;
            case "Green":
                if (greenWireLitUp != null)
                {
                    greenWireLitUp.SetActive(true);
                    Debug.Log("Green lit-up image enabled.");
                }
                else
                {
                    Debug.LogWarning("GreenWireLitUp is not assigned!");
                }
                break;
            default:
                Debug.LogWarning($"Unknown wire color: {color}");
                break;
        }
    }
}
