using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "StageInfo", menuName = "ScriptableObject/Stage")]
public class StageInfoSO : ScriptableObject
{
    [field: SerializeField] public int StageID { get; private set; }
    [field: SerializeField] public GameObject Map { get; private set; }
    [field: SerializeField] public GameObject Player { get; private set; }
    [field: SerializeField] public Vector3 PlayerStartPosition { get; private set; }
    [field: SerializeField] public List<StageEnemyInfo> MonsterInfos { get; private set; }

    public void InitStage()
    {
        CreateMap();
        CreatePlayer();
        CreateMonsters();
        GameManager.Instance.GameStart();
    }

    private void CreateMap()
    {
        Instantiate(Map);
    }

    private void CreatePlayer()
    {
        Instantiate(Player, PlayerStartPosition, Quaternion.identity);
    }

    private void CreateMonsters()
    {
        foreach (StageEnemyInfo monsterInfo in MonsterInfos)
        {
            EnemyController enemy = Instantiate(monsterInfo.EnemyPrefab, monsterInfo.PatrolWayPoints[0], Quaternion.identity);
            enemy.SetWayPoints(monsterInfo.PatrolWayPoints);
        }
    }
}
