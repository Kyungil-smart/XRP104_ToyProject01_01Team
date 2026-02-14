using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private Transform _muzzlePoint;
    [SerializeField] private float _bulletFlightDistance;
    
    private EnemyStats _stats;
    private bool _canAttack;
    private float _timeCount;

    private void Awake() => Init();
    private void Update() => Cooldown();

    private void Init()
    {
        _stats = GetComponent<EnemyStats>();
        
        _canAttack = true;
        _timeCount = 0;
    }

    public void Cooldown()
    {
        if (_canAttack) return;
        
        _timeCount += Time.deltaTime;
        if (_timeCount >= _stats.AttackCoolTime)
        {
            _canAttack = true;
            _timeCount = 0;
        }
    }

    public void Attack(Transform target)
    {
        if (!_canAttack) return;
        
        Instantiate(_stats.DefaultBullet)
            .SetData(
                _stats.BulletSpeed,
                _stats.AttackPower,
                _bulletFlightDistance,
                _stats.AttackTargetLayer,
                _muzzlePoint
                );
        
        _canAttack = false;
    }
}
