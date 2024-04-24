using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandom : MonoBehaviour
{
    public GameObject enemyPrefabs;
    


   

    public void SpawnEnemy(int spawnCount)
    {
        
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-50, 55), Random.Range(-33, -35));
            Instantiate(enemyPrefabs, spawnPosition, Quaternion.identity);
        }
      
    }

   
}
