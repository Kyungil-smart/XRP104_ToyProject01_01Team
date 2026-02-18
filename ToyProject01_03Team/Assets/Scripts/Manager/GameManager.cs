using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [field: SerializeField] public int CurrentStage { get; set; }
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
}