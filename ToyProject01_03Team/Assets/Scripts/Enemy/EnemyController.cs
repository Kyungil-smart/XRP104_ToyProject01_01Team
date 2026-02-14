using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(EnemyStats), typeof(EnemyMovement))]
public class EnemyController : MonoBehaviour, IDamagable, ITargetable
{
    public EnemyState State { get; private set; }
    public Transform Target { get; private set; }

    private EnemyStats _stats;
    private TargetDetector _targetDetector;
    private EnemyMovement _movement;

    private void Awake() => Init();
    private void OnEnable() => ConnectEvents();
    
    private void Update()
    {
        HandleState();
        HandleMovement();
    }
    
    private void OnDisable() => DisconnectEvents();

    private void OnDrawGizmos() => DrawLineToDetectedTarget();

    private void Init()
    {
        _stats = GetComponent<EnemyStats>();
        _movement = GetComponent<EnemyMovement>();
        _targetDetector = GetComponentInChildren<TargetDetector>();

        State = EnemyState.Patrol;
    }
    
    private void HandleState()
    {
        if (State == EnemyState.Chase)
        {
            float distance = Vector3.Distance(transform.position, Target.position);
            if (distance <= _stats.AttackRange)
            {
                // TODO: Attack 상태로 전환 및 공격 로직
            }
            else if (distance >= _stats.MissingRange)
            {
                State = EnemyState.Patrol;
            }
        }
    }

    private void HandleMovement()
    {
        if (State == EnemyState.Attack) return;
        
        switch (State)
        {
            case EnemyState.Chase: 
                _movement.StartMove(Target.position);
                break;
            case EnemyState.Patrol:
                _movement.Patrol();
                break;
        }
    }

    private void ChangeStateChase(Transform target)
    {
        State = EnemyState.Chase;
        Target = target;
        _movement.StartMove(target.position);
    }

    public void TakeDamage(float damage)
    {
        _stats.Health.Value -= damage;

        if (_stats.Health.Value <= 0) Die();
    }

    public void Die()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }

    private void ConnectEvents()
    {
        _targetDetector.OnTargetDetected += ChangeStateChase;
    }

    private void DisconnectEvents()
    {
        _targetDetector.OnTargetDetected -= ChangeStateChase;
    }

    private void DrawLineToDetectedTarget()
    {
        if (State != EnemyState.Chase) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, Target.position);
    }
}
