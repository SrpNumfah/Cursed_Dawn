using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    private Color originalColor;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalColor = meshRenderer.material.color;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstruction")
        {
            Color transparentColor = originalColor;
            transparentColor.a = 0f;
            meshRenderer.material.color = transparentColor;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Obstruction")
        {
            meshRenderer.material.color = originalColor;
        }
    }
}
