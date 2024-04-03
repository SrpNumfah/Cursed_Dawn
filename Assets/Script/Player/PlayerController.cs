using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public Animator animator;
    [Header("Move")]
    public float speed = 6f;
    public ParticleSystem walkDust;

    

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

    [Header("Skill Effects")]
    public ParticleSystem sheild;


    
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
            walkDust.Play();
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
            walkDust.Stop();
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


    #region PlayerHealth
    public void Health(int damage)
    {
        maxHealth -= damage;
        Debug.Log("Health" + maxHealth.ToString());
    }

    #endregion

    #region PlayerSkills
    public void Sheild()
    {
        StartCoroutine(TimeToUseSheild());
        
    }
    IEnumerator TimeToUseSheild()
    {
        yield return new WaitForSeconds(0.5f);
    }
    #endregion

}
