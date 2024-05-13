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
    public float healingHp = 15;
   
    [Header("GetDamage")]
    [SerializeField] SpriteRenderer playerSprite;
    public AudioSource Ondamage;

    [Header("Skill Sheild")]
    public ParticleSystem shield;
    public bool isShieldActive = false;
    public GameObject blockText;

    private void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        hud = FindObjectOfType<HUD_Manager>();
        merchants = FindObjectOfType<Merchants>();
        maxHealth = PlayerData.instance.maxHealth;
        attackDamage = PlayerData.instance.attackDamage;
        hud.Hpslider(PlayerData.instance.maxHealth);
    }

    void Update()
    {
        MoveMent();
        Attack();
        ActivedSheild();
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

            enemies.GetComponent<EnemyController>().TakeDamage(attackDamage);

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
        if (!isShieldActive)
        {
            PlayerData.instance.maxHealth -= damage;
            Ondamage.Play();
            playerSprite.color = Color.red;

          
            StartCoroutine(ChangeColorWhenTakingDamage());
            Debug.Log("Health" + PlayerData.instance.maxHealth.ToString());

        }
        else
        {
            Debug.Log("Shield blocked damage!" + damage);
            BlockingDamage();
        }

        hud.Hpslider(PlayerData.instance.maxHealth);

    }

    IEnumerator ChangeColorWhenTakingDamage()
    {
        yield return new WaitForSeconds(0.2f);
        playerSprite.color = Color.white;
    }

        #endregion


        #region Shield_Skill
        public void ActivedSheild()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isShieldActive = true;
                shield.Play();
                StartCoroutine(DeactivateShieldAfterDelay());
            }

        }
        public void BlockingDamage()
        {

            blockText.SetActive(true);
            StartCoroutine(TextPopUp());

        }

        IEnumerator DeactivateShieldAfterDelay()
        {
            yield return new WaitForSeconds(10f);
            isShieldActive = false;
            shield.Stop();
        }

        IEnumerator TextPopUp()
        {
            yield return new WaitForSeconds(0.5f);
            blockText.SetActive(false);
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
