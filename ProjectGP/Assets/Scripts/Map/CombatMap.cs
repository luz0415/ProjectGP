using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
            Quaternion spawnRotation = Quaternion.Euler(0f, Random.Range(0, 360), 0f);
            GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            SpawnEnemy(enemy, RandomPoint(spawnPoint, 1.0f), spawnRotation);
        }
    }

    private void SpawnSuburbEnemies(int enemyNumber)
    {
        for (int i = 0; i < enemyNumber; i++)
        {
            Vector3 spawnPoint = EnemySuburbSpawnPoints[Random.Range(0, EnemySuburbSpawnPoints.Length)].position;
            Quaternion spawnRotation = Quaternion.Euler(0f, Random.Range(0, 360), 0f);
            GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            SpawnEnemy(enemy, RandomPoint(spawnPoint, 1.0f), spawnRotation);
        }
    }

    Vector3 RandomPoint(Vector3 center, float range)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomDirection = new Vector3(Random.value, 0f, Random.value).normalized;
            Vector3 randomPoint = center + randomDirection * range;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }

        return Vector3.zero;
    }

    private void SpawnEnemy(GameObject enemy, Vector3 spawnPos, Quaternion spawnRotation)
    {
        Enemy newEnemy = Instantiate(enemy, spawnPos, spawnRotation).GetComponent<Enemy>();
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
