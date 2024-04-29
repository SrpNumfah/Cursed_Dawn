using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [Header("EnemyHealth")]
    public int maxHp = 100;
    public int currentHp;
    

    [Header("EnemyFollow")]
    public float lookRadius = 10f;
    public float attackRadius = 2f;
    public float attackSpeed = 1f;
    public float attackCooldown = 0f;
    public Transform attackPoint;
    public Animator enemyAnimation;
    bool canAttack = true;
    public LayerMask playerLayer;
    
    [Header("Boss Only")]
    public Slider bossHpBar;
   






    [Header("Ref")]
    public ParticleSystem portal;
    Transform target;
    [SerializeField] NavMeshAgent agent;
   
   
   // [SerializeField] RuneSpawner rune;


    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();


        if (portal != null)
        {
            portal.Play();
        }
        
       // rune = GameObject.FindObjectOfType<RuneSpawner>();
        agent.stoppingDistance = attackRadius; 

        currentHp = maxHp;



        attackPoint.gameObject.SetActive(false);

       
      

    }

    private void Update()
    {
        if (bossHpBar != null)
        {
            bossHpBar.value = currentHp;
        }
        float distance = Vector3.Distance(target.position, transform.position);

        enemyAnimation.SetBool("idle", true);
        


        if (distance <= lookRadius)
        {
            attackCooldown -= Time.deltaTime;


            enemyAnimation.SetBool("idle", false);
            enemyAnimation.SetBool("IsRun", true);
            
          
            agent.isStopped = false;
            agent.SetDestination(target.position);
           
            if (distance <= attackRadius)
            {
               
                AttackPlayer();
                

            }
            else
            {
                enemyAnimation.SetBool("IsAttack", false);
            }
         
            Vector3 lookPos = new Vector3(25,0,0);
            transform.eulerAngles = lookPos;

            if (agent.velocity.normalized.x > 0)
            {
                Vector3 scale = new Vector3(-2, 2, 2);
                transform.localScale = scale;
            }

            if (agent.velocity.normalized.x < 0)
            {
                Vector3 scale = new Vector3(2, 2, 2);
                transform.localScale = scale;
            }

           
        }
    
        else
        {
            agent.isStopped = true;
            enemyAnimation.SetBool("IsRun", false);

        }
    }

 

    public void AttackPlayer()
    {
       
         PlayerController playerController = target.GetComponent<PlayerController>();

         if (playerController != null)
         {
            if (currentHp <= 100)
            {
                enemyAnimation.SetBool("stageAttack", true);
                enemyAnimation.SetFloat("LoopAttack",3f);
            }
            else
            {
                if (attackCooldown <= 0f)
                {
                    enemyAnimation.SetBool("stageAttack", false);
                    enemyAnimation.SetBool("IsAttack", true);


                    Collider[] attackPlayer = Physics.OverlapSphere(attackPoint.position, attackRadius, playerLayer);


                    attackCooldown = 3f / attackSpeed;



                }
            }
           

         } 

    }

   
    
    
   
    

    public void TakeDamage(int damage)
    {
        
        currentHp -= damage;

        // เผื่อใส่ อนิเมชั่นตอนโดนตี หรือเปลี่ยนสี

       

        if (currentHp <= 0)
        {
            OnGoblinDie();
            
        }

    }

    void OnGoblinDie()
    {
        Debug.Log("Goblin ตุยเย่วาตานาเบ้ไอโกะ");
        // ใส่ Particle or Animation ตอนตาย
        // ตีต่อไม่ได้แล้ว ให้มันเป็นศพละเราไปเหยียบซ้ำ
        GetComponent<Collider>().enabled = false; 
        this.enabled = false;


        //  rune.Spawner();

        HUD_Manager hud = FindObjectOfType<HUD_Manager>();

        if (hud != null)
        {
            hud.GainExpFromEnemy(10);
        }

        Destroy(gameObject);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,lookRadius);

        if (attackPoint == null)
        {
            
            return;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);

    }

}
