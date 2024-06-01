using System.Collections;
using UnityEngine;


public class Skeleton : MonoBehaviour
{
    public int maxHp = 100;
    public int currentHp;
    public float detectionRadius = 10f;
    public float rotationSpeed = 5f;
    public float throwRate = 1f;
    public GameObject axePrefab;
    public Transform throwPoint;

    private Transform target;
    private float throwCooldown = 0f;
    [SerializeField] SpriteRenderer enemySprite;
    [SerializeField] Animator animator;


    private void Awake()
    {
        enemySprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        animator.SetBool("idle", true);
    }
    private void Update()
    {
        DetectTarget();
        if (target != null)
        {
            RotateTowardsTarget();
            FireAtTarget();
        }
    }

    private void DetectTarget()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, LayerMask.GetMask("Player"));
        if (hits.Length > 0)
        {
            animator.SetBool("idle", false);
            animator.SetBool("IsAttack", true);
            target = hits[0].transform;
        }
        else
        {
            target = null;
            animator.SetBool("IsAttack", false);
            animator.SetBool("idle", true);
        }
    }

    private void RotateTowardsTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private void FireAtTarget()
    {
        if (throwCooldown <= 0f)
        {
            Instantiate(axePrefab, throwPoint.position, throwPoint.rotation);
            throwCooldown = 1f / throwRate;
        }
        throwCooldown -= Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        enemySprite.color = Color.red;
        StartCoroutine(OnTakingDamageFormPlayer());
    }
    IEnumerator OnTakingDamageFormPlayer()
    {
        yield return new WaitForSeconds(0.1f);
        enemySprite.color = Color.white;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

}





