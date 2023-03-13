using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    public static Manager instance = null;
    
    public GameObject spawnPoint;
    public GameObject[] enemies;
    public int maxEnemiesOnScreen;
    public int totalEnemis;
    public int enemiesPerSpawn;


    private int _enemiesOnScreen = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    void Update()
    {
        Spawn();
    }

    void Spawn()
    {
        if (enemiesPerSpawn > 0 && _enemiesOnScreen < totalEnemis)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                if (_enemiesOnScreen < maxEnemiesOnScreen)
                {
                    GameObject newEnemy = Instantiate(enemies[0]) as GameObject;
                    newEnemy.transform.position = spawnPoint.transform.position;
                    _enemiesOnScreen++;
                }
            }
        }
    }
}
