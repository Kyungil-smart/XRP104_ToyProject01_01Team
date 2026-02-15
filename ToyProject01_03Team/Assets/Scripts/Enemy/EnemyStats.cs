using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] private float _defaultHealth;
    public ObservableProperty<float> Health = new ObservableProperty<float>();
    
    [Header("Movement")]
    [SerializeField] private float _defaultMoveSpeed;
    public float MoveSpeed { get; set; }
    [field: SerializeField] public float TurnSpeed { get; set; }
    
    [field: Header("Detection (FOV)")]
    [field: SerializeField] public float DetectRange { get; set; }
    [field: SerializeField, Range(0, 360)] public float ViewAngle { get; set; }
    [field: SerializeField] public float MissingRange { get; set; }

    [field: Header("Combat")]
    [field: SerializeField] public float AttackRange { get; set; }
    [field: SerializeField] public LayerMask AttackTargetLayer { get; set; }
    [field: SerializeField] public float AttackPower { get; set; }
    [field: SerializeField] public float AttackCoolTime { get; set; }
    
    [field: Header("Projectile")]
    [field: SerializeField] public Bullet DefaultBullet { get; set; }
    [field: SerializeField] public float BulletSpeed { get; set; }



    private void Awake() => Init();
    
    private void Init()
    {
        Health.Value = _defaultHealth;
        MoveSpeed = _defaultMoveSpeed;
    }
}
