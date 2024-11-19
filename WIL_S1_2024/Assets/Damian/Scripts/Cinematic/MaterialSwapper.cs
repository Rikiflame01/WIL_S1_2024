using System.Collections.Generic;
using UnityEngine;

public class MaterialSwapper : MonoBehaviour
{
    public GameObject[] prefabs;
    public Material targetMaterial;
    public Material replacementMaterial;

    void Start()
    {
        EventManager.Instance.TriggerUpgradeCinematicEvent.AddListener(StartReplacement);
    }

    void OnDisable()
    {
        EventManager.Instance.TriggerUpgradeCinematicEvent.RemoveListener(StartReplacement);
    }

    void StartReplacement(){
        foreach (GameObject prefab in prefabs)
        {
            GameObject[] instances = GameObject.FindGameObjectsWithTag(prefab.tag);

            foreach (GameObject instance in instances)
            {
                ReplaceMaterial(instance.transform);
            }
        }
    }

    void ReplaceMaterial(Transform parent)
    {
        MeshRenderer renderer = parent.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            Material[] materials = renderer.sharedMaterials;
            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i] == targetMaterial)
                {
                    materials[i] = replacementMaterial;
                }
            }
            renderer.sharedMaterials = materials;
        }

        foreach (Transform child in parent)
        {
            ReplaceMaterial(child);
        }
    }
}