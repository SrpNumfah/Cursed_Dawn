using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForBossSkills : MonoBehaviour
{
    [Header("Berserk")]
    public int berserkAttack = 10;
    public Transform attackPoint;




    public void Berserk_skill()
    {
        if (attackPoint != null)
        {
            attackPoint.gameObject.SetActive(true);
            FindObjectOfType<PlayerController>().Health(berserkAttack);
            Debug.Log("Attack player");
        } 
    }

    
   
}
