using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomLevels : MonoBehaviour
{
    
    
    public void LevelsRandom()
    {
            int index = Random.Range(2, 5);
  
            SceneManager.LoadScene(index);
            Debug.Log(index);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelsRandom();
        }
    }

}
