using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandom : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefabs;
    public float posX;
    public float posZ;
    public int enemyCount;

    private void Start()
    {
        StartCoroutine(OnSpawnEnemy());
    }

    IEnumerator OnSpawnEnemy()
    {

        while (enemyCount < 5)
        {
            posX = Random.Range(-25, 25);
            posZ = Random.Range(1, -40);
            Instantiate(enemyPrefabs, new Vector3(posX, 3.79f, posZ), Quaternion.identity);
            yield return new WaitForSeconds(0f);
            enemyCount += 1;
        }
        
    }
}
