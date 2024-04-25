using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD_Manager : MonoBehaviour
{
    [Header("PlayerHp")]
    public Slider slider;
    public TMP_Text hpText;

    [Header("CollectRune")]
    public TMP_Text runeText;

    [Header("Potion")]
    [SerializeField] int currentPotion = 0;
    public Image[] potions;
    public Sprite fullPotion;
    public Sprite emptyPotion;
    private const int healthToAddPerPotion = 10;
    [SerializeField] ParticleSystem healEffect;

    [Header("Pause_Menu")]
    public GameObject pauseMenuPanel;
    public static bool GameIsPause = false;
    

    [Header("EXP")]
    public Slider expSlider;
    public TMP_Text levelText;

    public int currentLevel = 1;
    public int currentExp = 0;
    public int expToLevelUp = 100;
    public int expIncreaseFactor = 2;

    [Header("Reference")]
    [SerializeField] PlayerController playerController;

    private void Start()
    {
        //LevelUp();
        OnUpdateRune();
        OnUpdatePotionUI();
        currentPotion = PlayerData.instance.currentPotion;

        GameObject heal = GameObject.Find("Healing");
        ParticleSystem particle = healEffect;
        healEffect = heal.GetComponent<ParticleSystem>();

        currentExp = PlayerData.instance.currentExp;


    }

    private void Update()
    {
        OnPauseMenu();
        EXPSlider();
        
    }

    #region HpSlider
    public void Hpslider(float value)
    {

        slider.value = value;
        hpText.text = value.ToString() + "/50";
        Debug.Log(slider.value);

        
    }
    #endregion

    #region Rune
    public void OnUpdateRune()
    {
        runeText.text = PlayerData.instance.rune.ToString();
    }
    #endregion

    #region Potion
    public void OnUpdatePotionUI()
    {
        for (int i = 0; i < potions.Length; i++)
        {
            if (i < PlayerData.instance.currentPotion)
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
            healEffect.Play();
            
            if (PlayerData.instance.currentPotion < potions.Length)
            {
                if (PlayerData.instance.maxHealth < PlayerData.instance.currentHealth)
                {
                    PlayerData.instance.maxHealth += healthToAddPerPotion;
                    PlayerData.instance.currentPotion++;
                    OnUpdatePotionUI();
                    Hpslider(PlayerData.instance.maxHealth);
                    Debug.Log(healthToAddPerPotion);
                }
                else
                {
                    Debug.Log("Health is already full!");
                }
                
            }
        }
       


    }
    #endregion

    #region Pause_Menu

    public void OnPauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause) // = true
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
    }
    public void Pause()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
    }
    #endregion

    #region EXP
    

    public void GainExpFromEnemy(int amount)
    {
        GainExp(amount);
    }
    void GainExp(int amount)
    {
        PlayerData.instance.currentExp += amount;
        while (PlayerData.instance.currentExp >= expToLevelUp)
        {
            LevelUp();
            
        }
    }

    void LevelUp()
    {
        currentLevel++;
        PlayerData.instance.currentExp -= expToLevelUp;
        expToLevelUp *= expIncreaseFactor;

    }

    public void EXPSlider()
    {
        expSlider.maxValue = expToLevelUp;
        expSlider.value = PlayerData.instance.currentExp;
        levelText.text = currentLevel.ToString();
    }

    #endregion
}
