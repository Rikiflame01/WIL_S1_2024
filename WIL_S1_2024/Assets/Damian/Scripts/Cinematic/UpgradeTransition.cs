using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Splines;

public class UpgradeTransition : MonoBehaviour
{
    public Camera mainCamera;
    public Camera transitionCamera;
    public Transform targetTransform;
    public SplineContainer splineContainer;
    public float duration = 5f;
    public CanvasGroup canvasGroup;

    private bool isTransitioning = false;
    private float elapsedTime = 0f;

    void Start()
    {
        EventManager.Instance.TriggerUpgradeCinematicEvent.AddListener(StartTransition);
    }

    void OnDestroy()
    {
                EventManager.Instance.TriggerUpgradeCinematicEvent.RemoveListener(StartTransition);
    }

    void Update()
    {
        if (isTransitioning)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            Vector3 position = splineContainer.EvaluatePosition(t);
            transitionCamera.transform.position = position;

            Vector3 directionToTarget = (targetTransform.position - position).normalized;
            Vector3 upVector = splineContainer.EvaluateUpVector(t);
            transitionCamera.transform.rotation = Quaternion.LookRotation(directionToTarget, upVector);

            if (elapsedTime <= 2f)
            {
                canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / 2f);
            }
            else if (elapsedTime >= duration - 2f)
            {
                canvasGroup.alpha = Mathf.Lerp(0f, 1f, (elapsedTime - (duration - 2f)) / 2f);
            }

            if (t >= 1f)
            {
                isTransitioning = false;
                elapsedTime = 0f;
                mainCamera.gameObject.SetActive(true);
                transitionCamera.gameObject.SetActive(false);
                canvasGroup.alpha = 0f;
            }
        }
    }

    void StartTransition()
    {
        mainCamera.gameObject.SetActive(false);
        transitionCamera.gameObject.SetActive(true);

        isTransitioning = true;
        elapsedTime = 0f;
    }

    [ContextMenu("Trigger Transition")]
    void TriggerTransitionFromInspector()
    {
        StartTransition();
    }
}