using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    private Color color;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        color = meshRenderer.material.color;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Color transparentColor = color;
            transparentColor.a = 0f;
            meshRenderer.material.color = transparentColor;
            Debug.Log("Player entered, object is now transparent.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            meshRenderer.material.color = color;
            Debug.Log("Player exited, object color reverted.");
        }
    }
}
