using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TapAnimationController : MonoBehaviour
{
    public Image tapTurnerImage;
    public Sprite[] tapTurnerFrames;
    public float tapTurnerFrameRate = 0.1f;

    public Image[] waterStreamImages;
    public Sprite[] waterStreamFrames;
    public float waterStreamFrameRate = 0.1f;

    public Image waterEndImage;
    public Sprite[] waterEndFrames;
    public float waterEndFrameRate = 0.1f;

    private Coroutine tapTurnerCoroutine;
    private Coroutine[] waterStreamCoroutines;
    private Coroutine waterEndCoroutine;

    void OnEnable()
    {
        StartAnimations();
    }

    void OnDisable()
    {
        StopAnimations();
    }

    void StartAnimations()
    {
        if (tapTurnerImage != null && tapTurnerFrames.Length > 0)
        {
            tapTurnerCoroutine = StartCoroutine(AnimateTapTurner());
        }

        if (waterStreamImages.Length > 0 && waterStreamFrames.Length > 0)
        {
            waterStreamCoroutines = new Coroutine[waterStreamImages.Length];
            for (int i = 0; i < waterStreamImages.Length; i++)
            {
                waterStreamCoroutines[i] = StartCoroutine(AnimateWaterStream(waterStreamImages[i]));
            }
        }

        if (waterEndImage != null && waterEndFrames.Length > 0)
        {
            waterEndCoroutine = StartCoroutine(AnimateWaterEnd());
        }
    }

    void StopAnimations()
    {
        if (tapTurnerCoroutine != null)
        {
            StopCoroutine(tapTurnerCoroutine);
            tapTurnerCoroutine = null;
        }

        if (waterStreamCoroutines != null)
        {
            foreach (var coroutine in waterStreamCoroutines)
            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
            }
            waterStreamCoroutines = null;
        }

        if (waterEndCoroutine != null)
        {
            StopCoroutine(waterEndCoroutine);
            waterEndCoroutine = null;
        }
    }

    IEnumerator AnimateTapTurner()
    {
        int frameIndex = 0;
        while (true)
        {
            tapTurnerImage.sprite = tapTurnerFrames[frameIndex];
            frameIndex = (frameIndex + 1) % tapTurnerFrames.Length;
            yield return new WaitForSeconds(tapTurnerFrameRate);
        }
    }

    IEnumerator AnimateWaterStream(Image waterStreamImage)
    {
        int frameIndex = 0;
        while (true)
        {
            waterStreamImage.sprite = waterStreamFrames[frameIndex];
            frameIndex = (frameIndex + 1) % waterStreamFrames.Length;
            yield return new WaitForSeconds(waterStreamFrameRate);
        }
    }

    IEnumerator AnimateWaterEnd()
    {
        int frameIndex = 0;
        while (true)
        {
            waterEndImage.sprite = waterEndFrames[frameIndex];
            frameIndex = (frameIndex + 1) % waterEndFrames.Length;
            yield return new WaitForSeconds(waterEndFrameRate);
        }
    }
}
