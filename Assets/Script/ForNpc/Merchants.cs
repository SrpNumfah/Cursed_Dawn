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
    [SerializeField] PlayerController playerController;

    [Header("UI")]
    public GameObject TalkBtn;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
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
}
