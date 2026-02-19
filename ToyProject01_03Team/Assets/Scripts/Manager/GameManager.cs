using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [field: SerializeField] public StageInfoSO CurrentStage { get; set; }
    private HashSet<EnemyController> _enemies = new HashSet<EnemyController>();
    public bool HasRemainingEnemies => _enemies.Count > 0;
    private float _startTime;
    public float ElapsedTime => -(_startTime - Time.time);
    
    public event Action OnGameStart;
    public event Action OnGamePause;
    public event Action OnGameResume;
    public event Action OnStageClear;
    public event Action OnGameOver;

    public bool IsGameRunning { get; private set; }
    public bool IsGamePaused { get; private set; }
    

    private void Awake()
    {
        SingletonInit();
        
        IsGameRunning = false;
        IsGamePaused = false;
    }

    public void GameStart()
    {
        IsGameRunning = true;
        _startTime = Time.time;
        OnGameStart?.Invoke();
    }

    public void GamePause()
    {
        if (!IsGameRunning) return;

        Time.timeScale = 0;
        IsGamePaused = true;
        OnGamePause?.Invoke();
    }

    public void GameResume()
    {
        if (!IsGameRunning) return;
        
        Time.timeScale = 1;
        IsGamePaused = false;
        OnGameResume?.Invoke();
    }

    public void GameOver()
    {
        IsGameRunning = false;
        OnGameOver?.Invoke();
    }

    public void StageClear()
    {
        IsGameRunning = false;
        OnStageClear?.Invoke();
    }
    
    public void AddEnemy(EnemyController enemy)
    {
        _enemies.Add(enemy);
    }

    public void RemoveEnemy(EnemyController enemy)
    {
        _enemies.Remove(enemy);
    }
}