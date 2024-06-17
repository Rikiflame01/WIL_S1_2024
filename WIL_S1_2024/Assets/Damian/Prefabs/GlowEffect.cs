using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class GlowEffect : MonoBehaviour
{
    [Tooltip("The speed of the glow effect.")]
    [SerializeField] private float glowSpeed = 1f;

    private Renderer objRenderer;
    private Material objMaterial;
    private Color baseColor = Color.white;
    private float glowTimer = 0f;

    void Start()
    {
        objRenderer = GetComponent<Renderer>();
        objMaterial = objRenderer.material;

        objMaterial.EnableKeyword("_EMISSION");

    }

    void Update()
    {
        float emissionStrength = Mathf.PingPong(glowTimer * glowSpeed, 1.0f);
        Color emissionColor = baseColor * Mathf.LinearToGammaSpace(emissionStrength);

        objMaterial.SetColor("_EmissionColor", emissionColor);

        glowTimer += Time.deltaTime;
    }
}
