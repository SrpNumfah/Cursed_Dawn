using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyController : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private Renderer objectRenderer;
    [SerializeField] private float transparency = 0.5f;
    private static readonly string BaseColorPropertyName = "_BaseColor";

    //cache
    private Color originalColor;
    private bool isTransparent = false;

    private void Awake()
    {
        originalColor = material.GetColor(BaseColorPropertyName);
    }

    #region Public
    public void SetTransParent()
    {
        if (!isTransparent)
        {
            OnsettingTransparent();
            isTransparent = true;
        }
    }
    public void ResetTransParent()
    {
        if (isTransparent)
        {
            OnResetTransparent();
            isTransparent = false;
        }
    }
    #endregion

    #region Private
    private void OnsettingTransparent()
    {
        Color transparentColor = originalColor;
        transparentColor.a = transparency;
        material.SetColor(BaseColorPropertyName, transparentColor);
        material.SetFloat("_Surface", 1); // Set surface type to Transparent
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;
    }

    private void OnResetTransparent()
    {
        material.SetColor(BaseColorPropertyName, originalColor);
        material.SetFloat("_Surface", 0); // Set surface type to Opaque
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        material.SetInt("_ZWrite", 1);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = -1;
    }
    #endregion
}
