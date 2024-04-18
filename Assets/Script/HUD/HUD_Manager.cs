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

    }

   

    public void Hpslider(float value)
    {

        slider.value = value;
        Debug.Log(slider.value);
    }

    
}
