using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomScene : MonoBehaviour
{
    public int sceneCount = 0;
    public int maxScene = 5;


    

    [Header("FadeInANDout")]
    public Animator animator;

    public void NextScene()
    {

        SceneManager.LoadScene(6);


    }

    public void OnRandom()
    {


        StartCoroutine(SceneRandom());



    }

    IEnumerator SceneRandom()
    {
        int index;
        do
        {
            index = Random.Range(2, 6);
            yield return null;

        } while (index == SceneManager.GetActiveScene().buildIndex || PlayerData.instance.loadedScenes.Contains(index));
        sceneCount++;
        PlayerPrefs.SetInt("scene", sceneCount);
        PlayerData.instance.loadedScenes.Add(index);

        SceneManager.LoadScene(index);
        Debug.Log(sceneCount + index);
        
       
    }


    private void Start()
    {
       
        sceneCount = PlayerPrefs.GetInt("scene", 1);
    }


    

   
    IEnumerator TimeToFade()
    {
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.5f);
        OnRandom();

    }
    IEnumerator FadeToBoss()
    {
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.5f);
        NextScene();
    }



    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && sceneCount < maxScene)
        {
            StartCoroutine(TimeToFade());


        }
        else
        {

            StartCoroutine(FadeToBoss());
        }
    }

   


}
