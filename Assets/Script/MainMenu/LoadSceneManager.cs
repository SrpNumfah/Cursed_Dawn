using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    
    [Header("FadeInANDout")]
    public Animator animator;

   
    public void FADE()
    {
       
        animator.SetTrigger("FadeOut");
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
