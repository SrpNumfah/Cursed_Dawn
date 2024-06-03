 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    
    public float fireRate;
    private float fireCooldown = 0f;
    public float detectionRadius = 10f;
    public GameObject fireBall;
    public Transform[] firePoints;
    [SerializeField] private HUD_Manager hud;


    private void Start()
    {
        hud = FindObjectOfType<HUD_Manager>();
    }
    void Update()
    {
        HandleFire();
        
    }

    #region private
    private void HandleFire()
    {
        if (Input.GetKeyDown(KeyCode.Space) && fireCooldown <= 0)
        {
            if (fireCooldown <= 0)
            {
                hud.SkillCoolDown1();
                StartCoroutine(FireRate());
                fireCooldown = 1f / fireRate;
            }
        }
        fireCooldown -= Time.deltaTime;
    }
  
    IEnumerator FireRate()
    {
        foreach (Transform firePoint in firePoints)
        {
            Instantiate(fireBall, firePoint.position, firePoint.rotation);
            Debug.Log("fire from " + firePoint.name);
        }
        yield return new WaitForSeconds(0.5f);
    }
    #endregion
}
