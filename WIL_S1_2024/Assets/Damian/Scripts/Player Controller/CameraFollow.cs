using UnityEngine;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour
{
    [Tooltip("The player transform to follow.")]
    public Transform player;

    [Tooltip("Offset of the camera relative to the player.")]
    public Vector3 offset;

    [Tooltip("Smoothness factor for camera movement.")]
    public float smoothSpeed = 0.125f;

    [Tooltip("Layer for objects that can obstruct the view.")]
    public LayerMask obstructionLayer;

    [Tooltip("Transparency level for obstructing objects.")]
    [Range(0f, 1f)]
    public float targetAlpha = 0.3f;

    [Tooltip("Lerp speed for transparency change.")]
    public float transparencyLerpSpeed = 5f;

    private List<Renderer> obstructedRenderers = new List<Renderer>();
    private Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>();

    private void LateUpdate()
    {
        if (player != null)
        {
            Vector3 desiredPosition = player.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(player);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsInObstructionLayer(other.gameObject))
        {
            Renderer renderer = other.GetComponent<Renderer>();
            if (renderer != null && !obstructedRenderers.Contains(renderer))
            {
                obstructedRenderers.Add(renderer);

                if (!originalMaterials.ContainsKey(renderer))
                {
                    originalMaterials[renderer] = renderer.materials;
                }

                foreach (Material mat in renderer.materials)
                {
                    SetMaterialTransparency(mat, targetAlpha);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsInObstructionLayer(other.gameObject))
        {
            Renderer renderer = other.GetComponent<Renderer>();
            if (renderer != null && obstructedRenderers.Contains(renderer))
            {
                obstructedRenderers.Remove(renderer);

                if (originalMaterials.ContainsKey(renderer))
                {
                    Material[] materials = originalMaterials[renderer];
                    for (int i = 0; i < materials.Length; i++)
                    {
                        SetMaterialTransparency(materials[i], 1f);
                    }
                }
            }
        }
    }

    private bool IsInObstructionLayer(GameObject obj)
    {
        return ((1 << obj.layer) & obstructionLayer.value) != 0;
    }

    private void SetMaterialTransparency(Material material, float targetAlpha)
    {
        Color color = material.color;
        color.a = Mathf.Lerp(color.a, targetAlpha, transparencyLerpSpeed * Time.deltaTime);
        material.color = color;

        if (targetAlpha < 1f)
        {
            material.SetFloat("_Mode", 2);
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        }
        else
        {
            material.SetFloat("_Mode", 0);
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            material.SetInt("_ZWrite", 1);
            material.DisableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = -1;
        }
    }
}
