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
    public LayerMask playerLayer;
    
    [Header("Boss Only")]
    public Slider bossHpBar;
    [SerializeField] EnemyRandom enemyRandom;
    






    [Header("Ref")]
    public ParticleSystem portal;
    [SerializeField] SpriteRenderer enemySprite;
    Transform target;
    [SerializeField] NavMeshAgent agent;
    public ParticleSystem death;
    public GameObject boss_hpBar;
    
   
   // [SerializeField] RuneSpawner rune;


    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        enemySprite = GetComponent<SpriteRenderer>();

        enemyRandom = FindObjectOfType<EnemyRandom>();
        if (bossHpBar != null)
        {
            enemyRandom.enabled = false;

        }

        if (boss_hpBar != null)
        {
            boss_hpBar.SetActive(true);
        }

      
       
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
        EnemyFollow();
    }
    #region EnemyFollow
    public void EnemyFollow()
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
                enemyAnimation.SetBool("stageAttack", false);
            }

            


            Vector3 isFlip = (target.position - transform.position).normalized;
            if (isFlip.x > 0)
            {
                enemySprite.flipX = false;
            } 
            else if (isFlip.y < 0)
            {
                enemySprite.flipX = true;
            }

           

        }

        else
        {
            agent.isStopped = true;
            enemyAnimation.SetBool("IsRun", false);

        }
    }
    #endregion

    #region DamageToPlayer
    public void AttackPlayer()
    {
       
         PlayerController playerController = target.GetComponent<PlayerController>();

        if (playerController != null)
        {

            if (attackCooldown <= 0f)
            {
                enemyAnimation.SetBool("stageAttack", false);
                enemyAnimation.SetBool("IsAttack", true);
               

                Collider[] attackPlayer = Physics.OverlapSphere(attackPoint.position, attackRadius, playerLayer);


                attackCooldown = 3f / attackSpeed;

            }
            if (bossHpBar != null)
            {
                if (currentHp <= 100)
                {
                    enemyAnimation.SetBool("stageAttack", true);
                    enemyAnimation.SetFloat("LoopAttack", 3f);
                    enemyRandom.enabled = true;
                   
                }
            }
          
        } 

    }


    #endregion


    #region OnEnemyTakeDamage

    public void TakeDamage(int damage)
    {
        
        currentHp -= damage;
        enemySprite.color = Color.red;
       
        StartCoroutine(OnTakingDamageFormPlayer());
       


       

        if (currentHp <= 0)
        {
            OnGoblinDie();
            if (bossHpBar != null)
            {
                bossHpBar.value = 0;
                boss_hpBar.SetActive(false);


            }
           
        }


    }
    IEnumerator OnTakingDamageFormPlayer()
    {
        yield return new WaitForSeconds(0.1f);
        enemySprite.color = Color.white;
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
        StartCoroutine(DeathEffectForBoss());
       

    }

    IEnumerator DeathEffectForBoss()
    {
        if (death != null)
        {
            death.Play();
            enemySprite.enabled = false;
            yield return new WaitForSeconds(1.5f);
            

        }

        Destroy(gameObject);
    }

    #endregion

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
