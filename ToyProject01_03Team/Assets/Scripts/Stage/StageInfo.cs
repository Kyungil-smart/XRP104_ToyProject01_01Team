using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class StageInfo : SceneSingleton<StageInfo>
{
    [field: SerializeField] public int StageNumber { get; private set; }
    [field: SerializeField] public GameObject Map { get; private set; }
    [field: SerializeField] public float StageTimeLimit { get; private set; }

    private HashSet<EnemyController> _enemies = new HashSet<EnemyController>();
    public event Action OnStageClear;
    public bool HasRemainingEnemies => _enemies.Count > 0;
    
    private void Awake()
    {
        SceneSingletonInit();
        Init();
    }

    private void Init()
    {
        Instantiate(Map, transform);
    }

    public void AddEnemy(EnemyController enemy)
    {
        _enemies.Add(enemy);
    }

    public void RemoveEnemy(EnemyController enemy)
    {
        _enemies.Remove(enemy);

        if (!HasRemainingEnemies)
        {
            OnStageClear?.Invoke();
        }
    }
}
