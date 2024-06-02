using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    public Animator animator;
    public AudioSource click;

    

    private void Start()
    {
        PlayerPrefs.DeleteAll();
    }

    
    public void FadeOut()
    {
        click.Play();
        animator.SetTrigger("fadeOut");
    }

    public void Map1()
    {
        
        SceneManager.LoadScene("Map1");
        PlayerData.instance.loadedScenes = new List<int>();
    }

    public void VoiceDemo()
    {
        click.Play();
        SceneManager.LoadScene("voice");
    }



    public void LeaveGame()
    {
        click.Play();
        Application.Quit();
        Debug.Log("LeaveGame");
    }


}
