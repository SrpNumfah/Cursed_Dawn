using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public Animator animator;
    [Header("Move")]
    public float speed = 6f;

    

    public EnemyController enemyController;

    [Header("Attack")]
    public float attackSpeed;
    public float attackRange = 0.5f;
    public float attackDamage;
    public Transform attackPoint;
    public LayerMask enemy;

   [Header("Health")]
    public float maxHealth = 50;
    public float healingHp = 15;


    
    void Update()
    {
        MoveMent();
        Attack();
    }
    #region MoveMent
    public void MoveMent()
    {
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f , vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
           
            animator.SetTrigger("Isrun");
            characterController.Move(direction * speed * Time.deltaTime);

            if (horizontal > 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(1f,1f,1f);
            }
        }
        else
        {
            animator.SetTrigger("idle");
        }

        characterController.Move(Physics.gravity * Time.deltaTime);
    }
    #endregion

    #region Attack
    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("Attack");

           Collider[] hitEnemy =  Physics.OverlapSphere(attackPoint.position,attackRange,enemy);

            foreach(Collider enemies in hitEnemy)
            {
                Debug.Log("We hit" + enemies.name);
            }
        }
       
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    #endregion

    public void Health(int damage)
    {
        maxHealth -= damage;
        Debug.Log("Health" + maxHealth.ToString());
    }

}
