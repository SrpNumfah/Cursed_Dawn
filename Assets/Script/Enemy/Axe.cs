using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;
    public int damage = 10;
    [SerializeField] Animator axeAnim;

    private void Awake()
    {
        axeAnim = GetComponent<Animator>();
    }
    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        axeAnim.Play(1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<PlayerController>().Health(damage);
            Debug.Log(damage);
            Destroy(gameObject);
        }
    }
}
