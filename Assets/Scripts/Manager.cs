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

    private const float spawnDelay = 0.9f;


    public List<Enemy> EnemyList = new List<Enemy>();


    void Start()
    {
        StartCoroutine(Spawn());
    }

    

    IEnumerator Spawn()
    {
        if (enemiesPerSpawn > 0 && EnemyList.Count < totalEnemis)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                if (EnemyList.Count < maxEnemiesOnScreen)
                {
                    GameObject newEnemy = Instantiate(enemies[0]);
                    newEnemy.transform.position = spawnPoint.transform.position;
                }
            }

            yield return new WaitForSeconds(spawnDelay);
            StartCoroutine(Spawn());
        }
    }

    public void RegicterEnemy(Enemy enemy)
    {
        EnemyList.Add(enemy);
    }
    public void UnRegicterEnemy(Enemy enemy)
    {
        EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    public void DestroyEnemies()
    {
        foreach (Enemy enemy in EnemyList)
        {
            Destroy(enemy.gameObject);
        }
        EnemyList.Clear();
    }
    
    

   
}
