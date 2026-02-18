using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [field: SerializeField] public int CurrentStage { get; set; }
    public event Action OnGameStart;
    public event Action<bool> OnGamePause;
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

    public void GamePause(bool isPaused)
    {
        if (!IsGameRunning) return;
        
        IsGamePaused = isPaused;
        OnGamePause?.Invoke(isPaused);
    }

    public void GameOver()
    {
        IsGameRunning = false;
        OnGameOver?.Invoke();
    }

    public void StageClear()
    {
        OnStageClear?.Invoke();
        GameOver();
    }
}