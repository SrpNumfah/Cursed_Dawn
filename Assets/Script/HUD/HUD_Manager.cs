using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    public AudioSource heal;

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


    [Header("DeathScene")]
    public GameObject deathUI;

    [Header("Reference")]
    [SerializeField] PlayerController playerController;
    

    private void Start()
    {
      
        OnUpdateRune();
        OnUpdatePotionUI();
        currentPotion = PlayerData.instance.currentPotion;
        deathUI.SetActive(false);
        pauseMenuPanel.SetActive(false);

        GameObject heal = GameObject.Find("Healing");
        ParticleSystem particle = healEffect;
        healEffect = heal.GetComponent<ParticleSystem>();

        

        currentExp = PlayerData.instance.currentExp;
        currentLevel = PlayerData.instance.currentLevel;

    }

    private void Update()
    {
        OnPauseMenu();
        EXPSlider();
        
    }

    #region HpSlider
    public void Hpslider()
    {

        slider.value = PlayerData.instance.currentHealth;
        hpText.text = PlayerData.instance.currentHealth.ToString() + "/50";
        Debug.Log(slider.value);

        if (slider.value <= 0)
        {
            PlayerDeath();
            Pause();
        }

        
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
            heal.Play();
            
            if (PlayerData.instance.currentPotion < potions.Length)
            {
                if (PlayerData.instance.currentHealth < PlayerData.instance.maxHealth)
                {
                    PlayerData.instance.currentHealth += healthToAddPerPotion;
                    PlayerData.instance.currentPotion++;
                    OnUpdatePotionUI();
                  
                    Debug.Log(healthToAddPerPotion);

                    if (PlayerData.instance.currentHealth > PlayerData.instance.maxHealth)
                    {
                        playerController.currentHealth = PlayerData.instance.maxHealth;
                    }

                    Hpslider();
                }
                else
                {
                    Debug.Log("Health is already full!");
                    heal.Stop();
                    healEffect.Stop();
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
    public void GoToLobby()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        PlayerData.instance.currentHealth = 50;
        PlayerData.instance.currentLevel = 0;
        PlayerData.instance.currentExp = 0;
        
    }
    public void ExitGame()
    {
        Application.Quit();
       
        Debug.Log("Quit");
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
        PlayerData.instance.currentLevel++;
        PlayerData.instance.currentExp -= expToLevelUp;
        expToLevelUp *= expIncreaseFactor;

    }

    public void EXPSlider()
    {
        expSlider.maxValue = expToLevelUp;
        expSlider.value = PlayerData.instance.currentExp;
        levelText.text = PlayerData.instance.currentLevel.ToString();
    }

    #endregion

    #region DeathScene
    public void PlayerDeath()
    {
        deathUI.SetActive(true);
    }

    public void Respawn()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        PlayerData.instance.currentHealth = (int)playerController.maxHealth;
        PlayerData.instance.currentLevel = 0;
        PlayerData.instance.currentExp = 0;
    }
    #endregion
}
