using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class Merchants : MonoBehaviour
{
    [Header("Refference")]
    private Animator merChantAnim;
    public CinemachineVirtualCamera targetGroup;
   
    [SerializeField] NPC npc;
    

    [Header("UI")]
    public GameObject TalkBtn;

    [Header("price")]
    public int price;

    private void Start()
    {
       
        merChantAnim = GetComponent<Animator>();
        npc = GetComponent<NPC>();
        TalkBtn.SetActive(false);
        targetGroup.gameObject.SetActive(false);
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
       // damageIncrease += PlayerData.instance.attackDamage;
        FindObjectOfType<PlayerController>().PlayerUpgradeDamage(damageIncrease);
    }
    public void UpgradeDamage(int prices)
    {
        OnUpgarde(10);
        price = PlayerData.instance.rune - prices;
        Debug.Log(PlayerData.instance.rune + price);
        
    }
}
