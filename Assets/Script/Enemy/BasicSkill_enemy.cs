using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSkill_enemy : MonoBehaviour
{
    [Header("Melee")]
    public Transform attackPoint;
    public int enemyDamage = 10;

   

    public void OnMeleeDamage()
    {
        if (attackPoint != null)
        {
            attackPoint.gameObject.SetActive(true);
            FindObjectOfType<PlayerController>().Health(enemyDamage);
            Debug.Log("Attack player");
        }
        else
        {
            attackPoint.gameObject.SetActive(false);
        }
       
    }

   

}
