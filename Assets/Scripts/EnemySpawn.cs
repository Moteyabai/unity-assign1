using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject elitePrefab;
    private Score score;

    public float normalSpawnInterval = 4f;
    public float eliteSpawnInterval = 10f;
    private bool isFacingRight = true;
    private float m_NormalSpawnTime = 3f;

    private void Start()
    {
        score = FindObjectOfType<Score>();
    }

    private void Update()
    {
        m_NormalSpawnTime -= Time.deltaTime;
        Debug.Log(score.Bonus);
        if (m_NormalSpawnTime <= 0 )
        {
            SpawnEnemy();
            m_NormalSpawnTime = normalSpawnInterval;
        }
        if(score.Bonus >= 10)
        {
            SpawnElite();
            score.Bonus = 0;
        }

        
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = CalculateSpawnPosition();
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        Destroy(enemy, 15);
    }

    private void SpawnElite()
    {
        Vector3 spawnPosition = CalculateSpawnPosition();
        GameObject eliteEnemy = Instantiate(elitePrefab, spawnPosition, Quaternion.identity);
        Destroy(eliteEnemy, 10);
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
