using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandom : MonoBehaviour
{
    public GameObject enemyPrefabs;
    public Transform[] spawnPoint;
    private GameObject[] _spawnEnemy;
    public int enemyCount;

    private void Start()
    {
        this.enemyCount = Random.Range(5, 10);
        SpawnEnemy(enemyCount);
       
    }

    public void SpawnEnemy(int enemyCount)
    {
        _spawnEnemy = new GameObject[enemyCount];
        

        for (int i = 0; i < enemyCount; i++)
        {
            int randomSpawmIndex = Random.Range(0, spawnPoint.Length);
            Vector3 randomPosition = spawnPoint[randomSpawmIndex].position;
            _spawnEnemy[i] = Instantiate(enemyPrefabs, randomPosition, Quaternion.identity);
            continue;
        }
    }

   
}
