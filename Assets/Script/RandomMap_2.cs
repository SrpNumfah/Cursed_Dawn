using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RandomMap_2 : MonoBehaviour
{
    public int sceneCount = 0;
    public int maxScene = 6;

    private void Start()
    {
        sceneCount = PlayerPrefs.GetInt("_scene", 1);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(13);
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
            index = Random.Range(9, 13);
            yield return null;

        } while (index == SceneManager.GetActiveScene().buildIndex || PlayerData.instance.loadedScenes.Contains(index));
        sceneCount++;
        PlayerPrefs.SetInt("_scene", sceneCount);
        PlayerData.instance.loadedScenes.Add(index);
        SceneManager.LoadScene(index);
        Debug.Log(sceneCount + index);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player") && sceneCount < maxScene)
        {
            OnRandom();
        }
        else
        {
            NextScene();
        }
    }
}
