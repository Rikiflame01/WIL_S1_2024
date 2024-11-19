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
        if (isSocket) return; //Sockets shouldn't be draggable

        canvasGroup.alpha = 0.6f; //Makes the wire semi-transparent
        canvasGroup.blocksRaycasts = false; //Allows other objects to detect the drag
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
        if (!isSocket) return; //Only sockets can detec

        GeneratorMiniGameLogic wire = eventData.pointerDrag.GetComponent<GeneratorMiniGameLogic>();
        if (wire != null && wire.wireColor == socketColor)
        {
            Debug.Log("Correct wire connected!");
            wire.transform.position = transform.position; //Snap the wire to the socket
            wire.enabled = false; //Disable further dragging
            FindObjectOfType<GeneratorMiniGameManager>().CheckCompletion();
        }
        else if (wire != null)
        {
            Debug.Log("Incorrect wire!");
        }
    }
}
