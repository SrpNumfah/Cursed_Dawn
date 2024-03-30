using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager instance;

    private void Awake()
    {
        if (instance = this)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    public GameObject player;
}
