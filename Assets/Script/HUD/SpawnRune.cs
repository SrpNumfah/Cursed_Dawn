using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRune : MonoBehaviour
{
    [SerializeField] GameObject runePrefabs;
    public int xPos;
    public int zPos;
    public int runesCount;

    

    private void Start()
    {
        StartCoroutine(RandomRunesPosition());
    }


    IEnumerator RandomRunesPosition()
    {
        while (runesCount < 10)
        {
            xPos = Random.Range(64, -60);
            zPos = Random.Range(30, -44);
            Instantiate(runePrefabs, new Vector3(xPos, 3.79f, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0f);
            runesCount += 1;
        }

    }

    public void RuneDrop(int dropCount)
    {
       
        Instantiate(runePrefabs, new Vector3(xPos,3.79f, zPos), Quaternion.identity);
    }
}
