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

    [Header("Potion")]
    [SerializeField] int currentPotion = 0;
    public Image[] potions;
    public Sprite fullPotion;
    public Sprite emptyPotion;
    private const int healthToAddPerPotion = 10;



    [Header("Reference")]
    [SerializeField] PlayerController playerController;

    private void Start()
    {
        OnUpdateRune();
        OnUpdatePotionUI();
        
    }

    private void Update()
    {
       
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
    public void OnUpdatePotionUI()
    {
        for (int i = 0; i < potions.Length; i++)
        {
            if (i < currentPotion)
            {
                potions[i].sprite = emptyPotion;
            }
            else
            {
                potions[i].sprite = fullPotion;
            }
        }
    }
    public void OnUsePotion()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentPotion < potions.Length)
            {
                PlayerData.instance.maxHealth += healthToAddPerPotion;
                currentPotion++;
                OnUpdatePotionUI();
                Hpslider(PlayerData.instance.maxHealth);
                Debug.Log(healthToAddPerPotion + currentPotion);
            }
        }

    }
    
}
