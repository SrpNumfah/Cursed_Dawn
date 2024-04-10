using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public int pointCount;
    public float maxRadius;
    public float speed;
    public float startWidth;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = pointCount + 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Wave());
        }
    }

    IEnumerator Wave()
    {
        float currentRadius = 0f;
        while (currentRadius < maxRadius)
        {
            currentRadius = +Time.deltaTime * speed;
            Draw(currentRadius);
            yield return null;
        }
    }

    public void Draw(float currentRadius)
    {
        float angleBetweenPoints = 360f / pointCount;

        for (int i = 0; i <= pointCount; i++)
        {
            float angle = i * angleBetweenPoints * Mathf.Rad2Deg;
            Vector3 direction = new Vector3(Mathf.Sin(angle),Mathf.Cos(angle),0f);
            Vector3 posotion = direction * currentRadius;

            lineRenderer.SetPosition(i,posotion);
        }
    }
}
