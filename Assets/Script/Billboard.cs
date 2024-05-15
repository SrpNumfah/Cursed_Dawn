using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    void Update()
    {
        // Get the camera transform
        Transform cameraTransform = FindObjectOfType<Camera>().gameObject.transform;

        // Calculate the direction vector from the sprite to the camera
        Vector3 directionToCamera = cameraTransform.position - transform.position;

        // Create a rotation that faces the camera
        Quaternion lookRotation = Quaternion.LookRotation(directionToCamera);

        // Apply the rotation to the sprite
        transform.rotation = lookRotation;
    }
}
