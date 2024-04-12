using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class Merchants : MonoBehaviour
{
    [Header("Refference")]
    private Animator merChantAnim;
    public CinemachineTargetGroup targetGroupCam;
   

    [Header("UI")]
    public GameObject TalkBtn;

    private void Start()
    {
        merChantAnim = GetComponent<Animator>();
        TalkBtn.SetActive(false);
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
        targetGroupCam.gameObject.SetActive(true);
        merChantAnim.SetBool("IsTriggerPlayer", true);
    }
}
