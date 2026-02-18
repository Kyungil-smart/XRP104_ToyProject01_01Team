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
    public bool HasRemainingEnemies => _enemies.Count > 0;

    private float _startTime;
    public float ElapsedTime => -(_startTime - Time.time);
    
    private void Awake()
    {
        SceneSingletonInit();
        Init();
    }
    
    private void Start() => GameManager.Instance.GameStart();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.GamePause();
        }
    }

    private void Init()
    {
        Instantiate(Map, transform);
        _startTime = Time.time;
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