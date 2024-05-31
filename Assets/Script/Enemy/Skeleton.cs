using UnityEngine;
using UnityEngine.AI;

public class Skeleton : MonoBehaviour
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
    
    public LayerMask playerLayer;

    [Header("Ref")]
    private Transform target;
    [SerializeField] private SpriteRenderer enemySprite;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator enemyAnimation;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        enemySprite = GetComponent<SpriteRenderer>();
        enemyAnimation = GetComponent<Animator>();
    }

    private void Start()
    {
        agent.stoppingDistance = attackRadius;
        currentHp = maxHp;
        attackPoint.gameObject.SetActive(false);
    }

    private void Update()
    {
        OnFollowPlayer();
    }

    #region Private
    private void OnFollowPlayer()
    {
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
                OnAttack();
            }
            else
            {
                enemyAnimation.SetBool("IsAttack", false);
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
    private void OnAttack()
    {
        PlayerController playerController = target.GetComponent<PlayerController>();

        if (playerController != null)
        {
            if (attackCooldown <= 0f)
            {
                enemyAnimation.SetBool("IsAttack", true);
                Collider[] attackPlayer = Physics.OverlapSphere(attackPoint.position, attackRadius, playerLayer);
                attackCooldown = 3f / attackSpeed;
            }
        }
    }
    #endregion
}





