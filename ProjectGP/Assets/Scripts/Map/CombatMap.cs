using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMap : Map
{
    [SerializeField] private GameObject CoinSpawnPoint;
    [SerializeField] private Transform[] EnemyCenterSpawnPoints;
    [SerializeField] private Transform[] EnemySuburbSpawnPoints;

    [SerializeField] private int spawnEnemyNumberMax;
    [SerializeField] private int spawnEnemyNumberMin;
    public int spawnEnemyNumber;

    public GameObject[] enemyPrefabs;

    public float centerSpawnRate = 0.75f;

    protected override void Start()
    {
        base.Start();
        CoinSpawnPoint.SetActive(false);
        GetSpawnEnemyNumber();
        SetMapReward();
        SpawnEnemies();
    }

    private void GetSpawnEnemyNumber()
    {
        spawnEnemyNumber = Random.Range(spawnEnemyNumberMin, spawnEnemyNumberMax);
    }

    private void SetMapReward()
    {
        Coin coin = CoinSpawnPoint.GetComponentInChildren<Coin>();
        for(int i = 0; i < spawnEnemyNumber; i++)
        {
            coin.coin += Random.Range(5, 10);
        }
    }

    private void SpawnEnemies()
    {
        int spawnCenterEnemyNumber = (int)(spawnEnemyNumber * centerSpawnRate);
        int spawnSuburbEnemyNumber = spawnEnemyNumber - spawnCenterEnemyNumber;
        SpawnCenterEnemies(spawnCenterEnemyNumber);
        SpawnSuburbEnemies(spawnSuburbEnemyNumber);
    }

    private void SpawnCenterEnemies(int enemyNumber)
    {
        for(int i = 0; i < enemyNumber; i++)
        {
            Vector3 spawnPoint = EnemyCenterSpawnPoints[Random.Range(0, EnemyCenterSpawnPoints.Length)].position;
            GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            SpawnEnemy(enemy, spawnPoint);
        }
    }

    private void SpawnSuburbEnemies(int enemyNumber)
    {
        for (int i = 0; i < enemyNumber; i++)
        {
            Vector3 spawnPoint = EnemySuburbSpawnPoints[Random.Range(0, EnemySuburbSpawnPoints.Length)].position;
            GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            SpawnEnemy(enemy, spawnPoint);
        }
    }

    private void SpawnEnemy(GameObject enemy, Vector3 spawnPos)
    {
        Enemy newEnemy = Instantiate(enemy, spawnPos, Quaternion.identity).GetComponent<Enemy>();
        newEnemy.onDeath += DecreaseEnemyCount;
    }

    private void Update()
    {
        if(spawnEnemyNumber <= 0)
        {
            isRoomEnd = true;
            EndRoom();
        }
    }

    protected override void EndRoom()
    {
        base.EndRoom();
        SpawnCoin();
    }

    private void SpawnCoin()
    {
        CoinSpawnPoint.SetActive(true);
    }
    
    public void DecreaseEnemyCount()
    {
        spawnEnemyNumber--;
    }
}
