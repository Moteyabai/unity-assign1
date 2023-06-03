using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    private bool isFacingRight = true;
    private float m_SpawnTime = 2f;
    

    private void Update()
    {
        
        m_SpawnTime -= Time.deltaTime;
        if (m_SpawnTime <= 0 )
        {
            SpawnEnemy();
            m_SpawnTime = spawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = CalculateSpawnPosition();
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
    
    

    private Vector3 CalculateSpawnPosition()
    {
        float width = Screen.width;
        float x;
        int ranNumber = Random.Range(-1, 1);
        x = ranNumber < 0 ? 100 : width - 100;
        Vector3 position = new Vector3(x, 100, Camera.main.nearClipPlane);
        return Camera.main!.ScreenToWorldPoint(position);
    }
   
}
