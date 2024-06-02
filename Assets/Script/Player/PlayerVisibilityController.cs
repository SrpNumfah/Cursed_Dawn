using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisibilityController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask obstructionLayer;
    [SerializeField] private float playerRadius = 0.5f;

    private void Update()
    {
        CheckObstructions();
    }
    #region Private
    private  void CheckObstructions()
    {
        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;
        Ray ray = new Ray(transform.position, direction);
        RaycastHit[] hits = Physics.SphereCastAll(ray, playerRadius, distance, obstructionLayer);

        foreach (RaycastHit hit in hits)
        {
            TransparencyController transparencyController = hit.collider.GetComponent<TransparencyController>();

            if (transparencyController != null)
            {
                transparencyController.SetTransParent();
            }
        }
        ResetTransparencyForNonHitObjects(hits);
    }

    private void ResetTransparencyForNonHitObjects(RaycastHit[] hits)
    {
        TransparencyController[] allTransparentObjects = FindObjectsOfType<TransparencyController>();

        foreach (TransparencyController transparencyController in allTransparentObjects)
        {
            bool isHit = false;

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.GetComponent<TransparencyController>() == transparencyController)
                {
                    isHit = true;
                    break;
                }
            }

            if (!isHit)
            {
                transparencyController.ResetTransParent();
            }
        }
    }
    #endregion
}
