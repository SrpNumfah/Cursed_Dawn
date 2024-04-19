using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerData.instance.rune++;
            FindObjectOfType<HUD_Manager>().OnUpdateRune();
            Destroy(gameObject);
        }
    }
}
