using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] Animator animator;
    public LayerMask playerLayer;
    Transform target;

    [Header("Boss Health")]
    public float maxHp = 1000f;
    public float currentHealth;

    [Header("Boss Follow")]
    public float lookRadius = 10f;
    public int enemyDamage = 10;
    public float attackRadius = 2f;
    public float attackSpeed = 1f;
    public float attackCooldown = 0f;
    public Transform attackPoint;

    [Header("ShockWave")]
    public ParticleSystem wave;
    public int shockWaveDamage = 5;

   
    bool canAttack;


    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.stoppingDistance = attackRadius;

       
        
    }

    private void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        animator.SetBool("idle" , true);

        if (distance <= lookRadius )
        {
            attackCooldown -= Time.deltaTime;

            animator.SetBool("idle", false);
            animator.SetBool("walk", true);


            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(target.position);

            if (distance <= attackRadius)
            {

                AttackToPlayer();

            }
            else
            {
                animator.SetBool("attack", false);
               
                // อาจใส่รีเลือดบอสเมื่อออกจากวง
            }

            Vector3 lookPos = new Vector3(25, 0, 0);
            transform.eulerAngles = lookPos;

            if (navMeshAgent.velocity.normalized.x > 0)
            {
                Vector3 scale = new Vector3(-2, 2, 2);
                transform.localScale = scale;
            }

            if (navMeshAgent.velocity.normalized.x < 0)
            {
                Vector3 scale = new Vector3(2, 2, 2);
                transform.localScale = scale;
            }
        }
        else
        {
            navMeshAgent.isStopped = true;
            animator.SetBool("walk", false);

        }
    }

    public void AttackToPlayer()
    {
        PlayerController player = target.GetComponent<PlayerController>();

        if (player != null)
        {
            if (attackCooldown <= 0) 
            {
                animator.SetBool("attack",true);
                
                Collider[] attackPlayer = Physics.OverlapSphere(attackPoint.position, attackRadius, playerLayer);

                FindObjectOfType<PlayerController>().Health(enemyDamage);
                Debug.Log("Hit player" + enemyDamage);
                attackCooldown = 3f / attackSpeed;
            }
        }
    }

    public void ShockWaveSkill()
    {
        FindObjectOfType<PlayerController>().Health(enemyDamage - shockWaveDamage);
    }


    public void OnTakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Debug.Log("I am death");
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

        if (attackPoint == null)
        {

            return;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);

    }
}
