using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnPointPlayer : MonoBehaviour
{
    
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform spawnPoint;
    private GameObject spawnedPlayer;
    

    public void Awake()
    {
        SpawnPlayer();
      
        
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
