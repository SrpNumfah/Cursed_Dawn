using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Manager : MonoBehaviour
{
    public Slider slider;
    [SerializeField] PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        playerController.maxHealth = PlayerData.instance.maxHealth;
        slider.value = PlayerData.instance.maxHealth;
       

    }

    
}
