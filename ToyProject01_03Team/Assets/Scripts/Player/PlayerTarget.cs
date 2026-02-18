using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerTarget : MonoBehaviour
{
    [SerializeField] private float targetRange;

    [SerializeField] private LayerMask _targetLayer;

    [SerializeField] private LayerMask _wallLayer;

    [SerializeField] private List<Transform> _enemies = new List<Transform>();

    public Transform Target { get; private set; }
    public bool IsDetectedEnemy { get; private set; }

    public event Action<bool> OnDetectedTargetInRange;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            _enemies.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            _enemies.Remove(other.transform);
        }
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.IsGameRunning) return;

        if(_enemies != null && _enemies.Count > 0)
        {
            Target = GetClosestEnemies();
        }
        else
        {
            Target = null;
        }
        
        OnDetectedTargetInRange?.Invoke(Target != null);

        if(Target != null)
        {
            if(IsBlockedByWall(Target))
            {
                Target = null;
            }
        }
        
        IsDetectedEnemy = Target != null;
    }
    
    private bool IsBlockedByWall(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        float distance = Vector3.Distance(transform.position, target.position);

        Ray ray = new Ray(transform.position, direction.normalized);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, distance, _wallLayer))
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        if(Target != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, Target.position);
        }
    }

    private Transform GetClosestEnemies()
    {
        // 리스트에 아무것도 안들어있거나, 리스트가 null이라면?
        if(_enemies.Count == 0 || _enemies == null) return null;

        // 리스트에 들어있는 첫 번째 몬스터와 거리를 미리 계산
        Transform closest = _enemies[0];
        float distance = Vector3.Distance(_enemies[0].position, transform.position);

        // 두번째 몬스터 부터 비교
        for(int i = 1; i < _enemies.Count; i++)
        {
            // 현재(i번째) 몬스터의 거리 계산
            float currentDistance = Vector3.Distance(_enemies[i].position, transform.position);

            // 현재 거리가 이전에 계산한 거리보다 적다면
            if(currentDistance < distance)
            {
                distance = currentDistance;
                closest = _enemies[i];
            }
        }
        return closest;
    }
}

