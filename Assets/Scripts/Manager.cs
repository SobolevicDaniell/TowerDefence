using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = System.Random;

public enum gameStatus
{
    next, play, gameover, win
}


public class Manager : Loader<Manager>
{

    [SerializeField] GameObject spawnPoint;
    [SerializeField] Enemy[] enemies;
    [SerializeField] int totalEnemis = 5;
    [SerializeField] private int totalWaves = 10;
    [SerializeField] private int enemiesPerSpawn = 1;
    [SerializeField] private int MaxEscaped;
    
    [SerializeField] private TMP_Text textTotalEscaped;
    [SerializeField] private TMP_Text textMoney;
    [SerializeField] private TMP_Text textWave;
    [SerializeField] private TMP_Text textBtn;
    [SerializeField] private Button playBtn;
    

    
    private int _waveNumber = 0;
    private int _totalMoney = 10;
    private int _totalEscaped = 0;
    private int _roundEscaped = 0;
    private int _totalKilled = 0;
    private int _whichEnemiesToSpawn = 0;
    private int _enemiesToSpawn = 0;

    private gameStatus _currentStatus = gameStatus.play;
    
    
    private const float spawnDelay = 0.9f;

    
    
    public int TotalEscaped
    {
        get
        {
            return _totalEscaped;
        }
        set
        {
            _totalEscaped = value;
        }
    }

    public int RoundEscaped
    {
        get
        {
            return _roundEscaped;
        }
        set
        {
            _roundEscaped = value;
        }
    }

    public int TotalKilled
    {
        get
        {
            return _totalKilled;
        }
        set
        {
            _totalKilled = value;
        }
    }
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
    
    
    
    public List<Enemy> EnemyList = new List<Enemy>();
    
    void Start()
    {
        playBtn.gameObject.SetActive(false);
        ShowMenu();
        textTotalEscaped.text = "Escaped " + TotalEscaped + "/" + MaxEscaped;
    }

    private void Update()
    {
        IfMaxEscaped();
    }

    IEnumerator Spawn()
    {
        if (enemiesPerSpawn > 0 && EnemyList.Count < totalEnemis)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                if (EnemyList.Count < totalEnemis)
                {
                    Random rnd = new Random();
                    Enemy newEnemy = Instantiate(enemies[rnd.Next(0, _enemiesToSpawn)]);
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

    public void IsWaveOver()
    {
        textTotalEscaped.text = "Escaped " + TotalEscaped + "/" + MaxEscaped;

        if ((RoundEscaped + TotalKilled) == totalEnemis)
        {
            if (_waveNumber <= enemies.Length)
            {
                _enemiesToSpawn = _waveNumber;
            }
            SetCurrentGameState();
            ShowMenu();
        }
    }

    public void SetCurrentGameState()
    {
        if (_totalEscaped >= 10)
        {
            _currentStatus = gameStatus.gameover;
        }
        else if (_waveNumber == 0 && (RoundEscaped + TotalKilled) == 0)
        {
            _currentStatus = gameStatus.play;
        }
        else if (_waveNumber >= totalWaves)
        {
            _currentStatus = gameStatus.win;
        }
        else
        {
            _currentStatus = gameStatus.next;
        }
    }


    public void PlayBtnPressed()
    {
        switch (_currentStatus)
        {
            case gameStatus.next:
                _waveNumber += 1;
                totalEnemis += _waveNumber;
                textWave.text = "Wave " + (_waveNumber + 1) + "/ " + (totalWaves + 1);
                textTotalEscaped.text = "Escaped " + TotalEscaped + "/" + MaxEscaped;
                
                break;
            
            default:
                totalEnemis = 5;
                TotalEscaped = 0;
                TotalMoney = 100;
                _enemiesToSpawn = 0;
                TowerManager.Instance.DestroyTowers();
                TowerManager.Instance.RenameTag();
                textMoney.text = TotalMoney.ToString();
                textTotalEscaped.text = "Escaped " + TotalEscaped + "/" + MaxEscaped;
                
                break;
        }
        DestroyEnemies();
        TotalKilled = 0;
        RoundEscaped = 0;
        textWave.text = "Wave " + (_waveNumber + 1) + "/ " + (totalWaves + 1);
        StartCoroutine(Spawn());
        playBtn.gameObject.SetActive(false);
    }
    
    
    
    public void ShowMenu()
    {
        switch (_currentStatus)
        {
            case gameStatus.gameover:
                textBtn.text = "Play again!";
                _waveNumber = 0;
                textWave.text = "Wave " + (_waveNumber + 1) + "/ " + (totalWaves + 1);
                
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

    public void IfMaxEscaped()
    {
        if (TotalEscaped >= MaxEscaped)
        {
            _currentStatus = gameStatus.gameover;
            ShowMenu();
        }
    }
    
}
