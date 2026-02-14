using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyStats), typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private EnemyStats _stats;
    private NavMeshAgent _agent;
    private EnemyController _controller;
    
    [SerializeField] private List<Vector3> _patrolWaypoints;
    private int _currentWaypointIndex;
    private int _indexAddValue;

    private void Awake() => Init();
    private void OnDrawGizmos() => DrawWaypoints();

    private void Init()
    {
        _stats = GetComponent<EnemyStats>();
        _agent = GetComponent<NavMeshAgent>();

        _currentWaypointIndex = 0;
        _indexAddValue = 1;
    }

    public void StartMove(Vector3 destination)
    {
        _agent.speed = _stats.MoveSpeed;
        _agent.angularSpeed = _stats.TurnSpeed;
        _agent.isStopped = false;
        
        _agent.SetDestination(destination);
    }

    public void StopMove()
    {
        _agent.isStopped = true;
    }

    public void Look(Transform target)
    {
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.LookRotation(target.position - transform.position),
            _stats.TurnSpeed * Time.deltaTime * 0.05f
        );
    }

    public void Patrol()
    {
        StartMove(_patrolWaypoints[_currentWaypointIndex]);

        if (Vector3.Distance(_patrolWaypoints[_currentWaypointIndex], transform.position) <= 0.5f)
        {
            HandleIndexAddValue();
            HandleCurrnetIndex();
        }
    }

    private void HandleCurrnetIndex()
    {
        _currentWaypointIndex += _indexAddValue;
    }

    private void HandleIndexAddValue()
    {
        if(_currentWaypointIndex == _patrolWaypoints.Count - 1) _indexAddValue = -1;
        else if (_currentWaypointIndex == 0) _indexAddValue = 1;
    }

    private void DrawWaypoints()
    {
        for (int i = 0; i < _patrolWaypoints.Count; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(_patrolWaypoints[i], 0.3f);
            
            if (i == 0) continue;
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(_patrolWaypoints[i - 1], _patrolWaypoints[i]);
        }
    }
}
