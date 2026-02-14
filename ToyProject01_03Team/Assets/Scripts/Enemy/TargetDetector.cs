using System;
using System.Collections;
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    public event Action<Transform> OnTargetDetected;
    
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private int _fovGizmoSegment;
    
    private Coroutine _coroutine;
    private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
    private Transform _target;
    
    [SerializeField] private EnemyStats _stats;
    private SphereCollider _collider;

    private void Awake() => Init();

    private void OnTriggerEnter(Collider other)
    {
        if(((1 << other.gameObject.layer) & _targetLayer) == 0) return;
        StartSearchTarget(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if(((1 << other.gameObject.layer) & _targetLayer) == 0) return;
        StopSearchTarget();
    }

    private void OnDrawGizmos()
    {
        DrawArc(_stats.DetectRange, Color.yellow);
        DrawArc(_stats.AttackRange, Color.red);
        DrawArc(_stats.MissingRange, Color.blue);
    }

    private void StartSearchTarget(Collider other)
    {
        if (_coroutine != null) return;

        _target = other.transform;
        _coroutine = StartCoroutine(SearchRoutine());
    }

    private void StopSearchTarget()
    {
        if (_coroutine == null) return;

        _target = null;
        StopCoroutine(_coroutine);
        _coroutine = null;
    }

    private IEnumerator SearchRoutine()
    {
        while (true)
        {
            yield return _waitForFixedUpdate;

            if (_target == null) yield break;
            
            Vector3 vectorToTarget = _target.position - transform.position;
            Vector3 directionToTarget = vectorToTarget.normalized;
            float distanceToTarget = vectorToTarget.magnitude;

            if (distanceToTarget > _stats.DetectRange * _stats.DetectRange) continue;

            float dot = Vector3.Dot(transform.forward, directionToTarget);
            float cosHalf = Mathf.Cos(_stats.ViewAngle * 0.5f * Mathf.Deg2Rad);

            if (dot >= cosHalf)
            {
                Ray ray = new Ray(transform.position, directionToTarget);
                RaycastHit hit;

                if (!Physics.Raycast(ray, out hit, distanceToTarget)) continue;
                if (((1 << hit.transform.gameObject.layer) & _targetLayer) == 0) continue;

                OnTargetDetected?.Invoke(hit.transform);
                _coroutine = null;
                yield break;
            }
        }
    }

    private void Init()
    {
        _stats = GetComponentInParent<EnemyStats>();
        _collider = GetComponent<SphereCollider>();
        
        _collider.radius = _stats.DetectRange;
        _coroutine = null;
    }

    private void DrawArc(float range, Color color)
    {
        Vector3 planarForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up);

        if (planarForward.sqrMagnitude < 0.001f) return;

        float halfAngle = _stats.ViewAngle * 0.5f;

        Gizmos.color = color;
        
        Vector3 prevDir = Quaternion.AngleAxis(-halfAngle, Vector3.up) * planarForward;
        Vector3 prev = transform.position + prevDir * range;
        
        Vector3 left = Quaternion.Euler(0, -_stats.ViewAngle * 0.5f, 0) * transform.forward;
        Vector3 right = Quaternion.Euler(0, _stats.ViewAngle * 0.5f, 0) * transform.forward;
        Gizmos.DrawLine(transform.position, transform.position + left * range);
        Gizmos.DrawLine(transform.position, transform.position + right * range);

        for (int i = 1; i <= _fovGizmoSegment; i++)
        {
            float t = (float)i / _fovGizmoSegment;
            float angle = Mathf.Lerp(-halfAngle, halfAngle, t);
            
            Vector3 dir = Quaternion.AngleAxis(angle, Vector3.up) * planarForward;
            Vector3 cur = transform.position + dir * range;

            Gizmos.DrawLine(prev, cur);
            prev = cur;
        }
    }
}
