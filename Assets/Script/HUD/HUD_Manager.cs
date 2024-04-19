using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD_Manager : MonoBehaviour
{
    [Header("PlayerHp")]
    public Slider slider;

    [Header("CollectRune")]
    public TMP_Text runeText;


    [Header("Reference")]
    [SerializeField] PlayerController playerController;

    private void Start()
    {
        OnUpdateRune();
    }

   

    public void Hpslider(float value)
    {

        slider.value = value;
        Debug.Log(slider.value);
    }

    public void OnUpdateRune()
    {
        runeText.text = PlayerData.instance.rune.ToString();
    }

    
}
