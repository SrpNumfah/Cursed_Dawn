using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
        
    }


    public void FadeOut()
    {
        animator.SetTrigger("fadeOut");
    }

    public void Map1()
    {
        SceneManager.LoadScene("Map1");
       
    }



    public void LeaveGame()
    {
        Application.Quit();
        Debug.Log("LeaveGame");
    }


}
