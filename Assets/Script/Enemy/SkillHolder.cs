using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHolder : MonoBehaviour
{
    [Header("Malee")]
    
    public Transform attackPoint;
    public int enemyDamage = 10;



    public void OnMaleeDamage()
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
