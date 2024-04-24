using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnPointPlayer : MonoBehaviour
{
    public EnemyRandom enemyRandom;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform spawnPoint;
    private GameObject spawnedPlayer;
    

    public void Awake()
    {
        SpawnPlayer();
      
        
    }

    private void Start()
    {
        


        if (enemyRandom != null)
        {
            enemyRandom.SpawnEnemy(5);
            Debug.Log(enemyRandom);
        }
        
    }

    public void SpawnPlayer()
    {
        if (spawnedPlayer != null)
        {
            Destroy(spawnedPlayer);
        }

        spawnedPlayer = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
   
    }
}
