using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("Reference")]
    public CharacterController characterController;
    public Animator animator;
    [SerializeField] Merchants merchants;
    [SerializeField] HUD_Manager hud;
   
    [Header("Move")]
    public float speed = 6f;
    public ParticleSystem walkDust;
    public AudioSource footStep;
    
    [Header("Attack")]
    public float attackSpeed;
    public float attackRange = 0.5f;
    public int attackDamage = 5;
    public AudioSource attackSound;
   
    public Transform attackPoint;
    public LayerMask enemy;

   [Header("Health")]
    public float maxHealth = 50;
    public float currentHealth;
    public float healingHp = 15;
   
    [Header("GetDamage")]
    [SerializeField] SpriteRenderer playerSprite;
    public AudioSource Ondamage;

    [Header("Skill")]
    public ParticleSystem shield;

    private void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        hud = FindObjectOfType<HUD_Manager>();
        merchants = FindObjectOfType<Merchants>();
        currentHealth = maxHealth;
        currentHealth = PlayerData.instance.currentHealth;
        maxHealth = PlayerData.instance.maxHealth;
        attackDamage = PlayerData.instance.attackDamage;
        hud.Hpslider();
    }

    void Update()
    {
        MoveMent();
        Attack();
        TalkingToNPC();
        hud.OnUsePotion();
    }


    #region MoveMent
    public void MoveMent()
    {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

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
                    transform.localScale = new Vector3(1f, 1f, 1f);
                }
            }
            else
            {
                animator.SetTrigger("idle");
                walkDust.Stop();
            }

            characterController.Move(Physics.gravity * Time.deltaTime);
    }

    public void FootStepSound()
    {
        footStep.Play();
    }

    #endregion

    #region Attack
    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("Attack");
        }

    }

    public void PlayAttackSound()
    {
        attackSound.Play();
    }

    public void TimeToUseAttack()
    {
        Collider[] hitEnemy = Physics.OverlapSphere(attackPoint.position, attackRange, enemy);

        foreach (Collider enemies in hitEnemy)
        {
            Debug.Log("We hit" + enemies.name + attackDamage);

            EnemyController enemyController = enemies.GetComponent<EnemyController>();
            Skeleton skeleton = enemies.GetComponent<Skeleton>();

            if (enemyController != null)
            {
                enemyController.TakeDamage(attackDamage);
            }
            else if (skeleton != null)
            {
                skeleton.TakeDamage(attackDamage);
            }


        }
    }
    public void PlayerUpgradeDamage(int damageIncrease)
    {
        PlayerData.instance.attackDamage += damageIncrease;
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    #endregion

    #region PlayerHealth
    public void Health(int damage)
    {
        PlayerData.instance.currentHealth -= damage;
        Ondamage.Play();
        playerSprite.color = Color.red;
        StartCoroutine(ChangeColorWhenTakingDamage());
        Debug.Log("Health" + PlayerData.instance.currentHealth.ToString());
        hud.Hpslider();
    }

    IEnumerator ChangeColorWhenTakingDamage()
    {
        yield return new WaitForSeconds(0.2f);
        playerSprite.color = Color.white;
    }
    #endregion

    #region TalkWithNpc
    public void TalkingToNPC()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            merchants.TalkingToPlayer();
        }
    }
    #endregion

}
