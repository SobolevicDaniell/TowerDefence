using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : Loader<Manager>
{

    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject[] enemies;
    [SerializeField] int maxEnemiesOnScreen;
    [SerializeField] int totalEnemis;
    [SerializeField] int enemiesPerSpawn;


    private int _enemiesOnScreen = 0;
    private const float spawnDelay = 0.9f;

    

    void Start()
    {
        StartCoroutine(Spawn());
    }

    

    IEnumerator Spawn()
    {
        if (enemiesPerSpawn > 0 && _enemiesOnScreen < totalEnemis)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                if (_enemiesOnScreen < maxEnemiesOnScreen)
                {
                    GameObject newEnemy = Instantiate(enemies[1]);
                    newEnemy.transform.position = spawnPoint.transform.position;
                    _enemiesOnScreen++;
                }
            }

            yield return new WaitForSeconds(spawnDelay);
            StartCoroutine(Spawn());
        }
    }

    public void RemoveEnemy()
    {
        if (_enemiesOnScreen > 0)
        {
            _enemiesOnScreen--;
        }
    }
}
