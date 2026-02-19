using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StageEnemyInfo
{
    [SerializeField] public EnemyController EnemyPrefab;
    [field: SerializeField] public List<Vector3> PatrolWayPoints { get; private set; }
}
