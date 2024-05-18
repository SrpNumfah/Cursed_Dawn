using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject endScene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            endScene.SetActive(true);
        }
    }
}
