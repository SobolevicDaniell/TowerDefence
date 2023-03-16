using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum gameStatus
{
    next, play, gameover, win
}


public class Manager : Loader<Manager>
{

    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject[] enemies;
    [SerializeField] int maxEnemiesOnScreen;
    [SerializeField] int totalEnemis;
    [SerializeField] private int totalWaves = 10;
    
    [SerializeField] private TMP_Text textTotalEscaped;
    [SerializeField] private TMP_Text textMoney;
    [SerializeField] private TMP_Text textWave;
    [SerializeField] private TMP_Text textBtn;
    [SerializeField] private Button playBtn;

    private int _enemiesPerSpawn;
    private int _waveNumber = 0;
    private int _totalMoney = 10;
    private int _totalEscaped = 0;
    private int _roundEscaped = 0;
    private int _totalKilled = 0;
    private int _whichEnemiesToSpawn = 0;

    private gameStatus _currentStatus = gameStatus.play;
    
    
    private const float spawnDelay = 0.9f;


    public List<Enemy> EnemyList = new List<Enemy>();

    public int TotalMoney
    {
        get
        {
            return _totalMoney;
        }
        set
        {
            _totalMoney = value;
            textMoney.text = _totalMoney.ToString();
        }
    }
    
    
    void Start()
    {
        playBtn.gameObject.SetActive(false);
        ShowMenu();
    }

    

    IEnumerator Spawn()
    {
        if (_enemiesPerSpawn > 0 && EnemyList.Count < totalEnemis)
        {
            for (int i = 0; i < _enemiesPerSpawn; i++)
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

    public void AddMoney(int amount)
    {
        TotalMoney += amount;
    }

    public void SubrractMoney(int amount)
    {
        TotalMoney -= amount;
    }

    public void ShowMenu()
    {
        switch (_currentStatus)
        {
            case gameStatus.gameover:
                textBtn.text = "Play again!";
                
                break;
            
            case gameStatus.next:
                textBtn.text = "Next wave";
                
                break;
            
            case gameStatus.play:
                textBtn.text = "Play game";
                
                break;
            
            case gameStatus.win:
                textBtn.text = "Play game";
                
                break;
        }
        playBtn.gameObject.SetActive(true);
    }
    
}
