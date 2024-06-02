using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;
    public int damage = 10;
    public Transform target;
    public float rotationSpeed = 5f;



    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    private void Update()
    {
        if (target != null)
        {
            RotateTowardsTarget();
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject); 
        }
    }
    private void RotateTowardsTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("Hit enemy with damage: " + damage);
            }
            Destroy(gameObject);
        }
    }
}
