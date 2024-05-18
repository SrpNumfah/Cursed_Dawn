using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Clip : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    private void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += ReturnToMenu;
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            ReturnToMenu(videoPlayer);
        }
    }

    void ReturnToMenu(VideoPlayer video)
    {
       
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;

    }
}
