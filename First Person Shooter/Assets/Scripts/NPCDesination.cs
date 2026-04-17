using UnityEngine;

public class NPCDesination : MonoBehaviour
{
    MeshRenderer meshRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }
}
