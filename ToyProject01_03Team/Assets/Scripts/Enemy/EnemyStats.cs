using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private float _defaultHealth;
    public ObservableProperty<float> Health = new ObservableProperty<float>();
    
    [SerializeField] private float _defaultMoveSpeed;
    public float MoveSpeed { get; set; }

    
    [field: SerializeField] public float DetectRange { get; set; }
    [field: SerializeField][field: Range(0, 360)] public float ViewAngle { get; set; }
    [field: SerializeField] public float AttackRange { get; set; }
    [field: SerializeField] public float MissingRange { get; set; }
    
    private void Awake() => Init();
    
    private void Init()
    {
        Health.Value = _defaultHealth;
        MoveSpeed = _defaultMoveSpeed;
    }
}
