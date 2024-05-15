using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;

    public int maxHealth;
    public int currentHealth;
    public int attackDamage;
    public int rune;
    public int currentPotion;
    public int currentExp;
    public int currentLevel;

    public List<int> loadedScenes = new List<int>();

    private void Awake()
    {
        if (instance == null)
        {
            maxHealth = 50;
            attackDamage = 5;
            currentHealth = maxHealth;
            


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
