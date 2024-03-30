using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;
    public int maxHealth;
    public List<int> loadedScenes = new List<int>();

    private void Awake()
    {
        if (instance == null)
        {
            maxHealth = 50;
            PlayerPrefs.DeleteAll();

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
