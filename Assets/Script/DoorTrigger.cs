using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{

    public Animator animator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(FadeTime());
        }
    }

    IEnumerator FadeTime()
    {
        animator.SetTrigger("fadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(8);
        PlayerData.instance.loadedScenes = new List<int>();
        Debug.Log("loadescene");
    }
}
