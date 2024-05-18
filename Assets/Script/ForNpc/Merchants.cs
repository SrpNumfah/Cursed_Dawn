using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class Merchants : MonoBehaviour
{
    [Header("Refference")]
    private Animator merChantAnim;
    public CinemachineVirtualCamera targetGroup;
    [SerializeField] ParticleSystem buff;
   
    [SerializeField] NPC npc;
    

    [Header("UI")]
    public GameObject TalkBtn;
    

   

    private void Start()
    {
       
        merChantAnim = GetComponent<Animator>();
        npc = GetComponent<NPC>();
        TalkBtn.SetActive(false);
        targetGroup.gameObject.SetActive(false);


        GameObject particle = GameObject.Find("Buff");
        ParticleSystem particleSystem = buff;
        buff = particle.GetComponent<ParticleSystem>();

    }

   
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            TalkBtn.SetActive(true);
            

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            merChantAnim.SetBool("IsTriggerPlayer", false);
            
        }
    }


    public void TalkingToPlayer()
    {
        TalkBtn.SetActive(false);

        targetGroup.gameObject.SetActive(true);
        npc.TriggerDialogue();
        merChantAnim.SetBool("IsTriggerPlayer", true);
    }

    public void OnUpgarde(int damageIncrease)
    {
        FindObjectOfType<PlayerController>().PlayerUpgradeDamage(damageIncrease);
    }
    public void UpgradeDamage(int prices)
    {
        if (PlayerData.instance.rune > prices)
        {
            OnUpgarde(10);
            PlayerData.instance.rune -= prices;
            Debug.Log(PlayerData.instance.rune);
            FindObjectOfType<HUD_Manager>().OnUpdateRune();
            buff.Play();
        }
        else
        {
            // เพิ่ม ประโยคว่า ไปหา rune เพิ่ม
            buff.Stop();
        }
       
    }

    public void BuyPotion(int price)
    {
        if (PlayerData.instance.rune >= price && PlayerData.instance.currentPotion > 0)
        {
            PlayerData.instance.rune -= price;
            PlayerData.instance.currentPotion--;
            FindObjectOfType<HUD_Manager>().OnUpdatePotionUI();
            FindObjectOfType<HUD_Manager>().OnUpdateRune();
            Debug.Log(PlayerData.instance.rune);
        }
        
    }
}
