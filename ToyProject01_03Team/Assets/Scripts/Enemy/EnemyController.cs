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
    private EnemyWeapon _weapon;

    private void Awake() => Init();
    private void OnEnable() => ConnectEvents();
    
    private void Update()
    {
        HandleState();
        HandleStateAction();
    }
    
    private void OnDisable() => DisconnectEvents();

    private void OnDrawGizmos() => DrawLineToDetectedTarget();

    private void Init()
    {
        _stats = GetComponent<EnemyStats>();
        _movement = GetComponent<EnemyMovement>();
        _weapon = GetComponent<EnemyWeapon>();
        _targetDetector = GetComponentInChildren<TargetDetector>();

        State = EnemyState.Patrol;
    }
    
    private void HandleState()
    {
        if (State == EnemyState.Chase)
        {
            float distance = Vector3.Distance(Target.position, transform.position);
            
            if (distance <= _stats.AttackRange)         State = EnemyState.Attack;
            else if (distance >= _stats.MissingRange)   State = EnemyState.Patrol;
        }

        if (State == EnemyState.Attack)
        {
            float distance = Vector3.Distance(Target.position, transform.position);

            if (distance >= _stats.MissingRange) State = EnemyState.Patrol;
            else if (distance >= _stats.AttackRange) State = EnemyState.Chase;
        }
    }

    private void HandleStateAction()
    {
        switch (State)
        {
            case EnemyState.Chase: 
                _movement.StartMove(Target.position);
                break;
            case EnemyState.Patrol:
                _movement.Patrol();
                break;
            case EnemyState.Attack:
                _movement.StopMove();
                _movement.Look(Target);
                _weapon.Attack(Target);
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
        if (State == EnemyState.Chase || State == EnemyState.Attack)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, Target.position);
        }
    }
}
