using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMap : Map
{
    [SerializeField] private GameObject CoinSpawnPoint;
    [SerializeField] private GameObject[] EnemyCenterSpawnPoints;
    [SerializeField] private GameObject[] EnemySuburbSpawnPoints;
    [SerializeField] private int spawnEnemyNumberMax;
    [SerializeField] private int spawnEnemyNumberMin;
    private int spawnEnemyNumber;

    protected override void Start()
    {
        base.Start();
        CoinSpawnPoint.SetActive(false);
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {

    }

    protected override void StartRoom()
    {
        base.StartRoom();
    }

    private void SpawnCoin()
    {
        CoinSpawnPoint.SetActive(true);
        // 코인 Collider가 Player와 부딪히면
    }

    protected override void EndRoom()
    {
        base.EndRoom();
        SpawnCoin();
    }
    
}
