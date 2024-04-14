using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("Reference")]
    public CharacterController characterController;
    public Animator animator;
    [SerializeField] Merchants merchants;
   

    [Header("Move")]
    public float speed = 6f;
    public ParticleSystem walkDust;

    

    [Header("Attack")]
    public float attackSpeed;
    public float attackRange = 0.5f;
    public int attackDamage = 5;
   
    public Transform attackPoint;
    public LayerMask enemy;

   [Header("Health")]
    public float maxHealth = 50;
    public float healingHp = 15;

    [Header("Skill Sheild")]
    public ParticleSystem shield;
    public bool isShieldActive = false;
    public GameObject blockText;

    private void Start()
    {
        merchants = FindObjectOfType<Merchants>();
        maxHealth = PlayerData.instance.maxHealth;
        attackDamage = PlayerData.instance.attackDamage;

    }

    void Update()
    {
        MoveMent();
        Attack();
        ActivedSheild();
        TalkingToNPC();

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
        #endregion

        #region Attack
        public void Attack()
        {

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                animator.SetTrigger("Attack");

                Collider[] hitEnemy = Physics.OverlapSphere(attackPoint.position, attackRange, enemy);

                foreach (Collider enemies in hitEnemy)
                {
                    Debug.Log("We hit" + enemies.name + attackDamage);

                    enemies.GetComponent<EnemyController>().TakeDamage(attackDamage);
                

                }
            }

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
                maxHealth -= damage;
                Debug.Log("Health" + maxHealth.ToString());
            }
            else
            {
                Debug.Log("Shield blocked damage!" + damage);
                BlockingDamage();
            }



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
