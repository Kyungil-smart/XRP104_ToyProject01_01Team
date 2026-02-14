using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private float _defaultHealth;
    public ObservableProperty<float> Health = new ObservableProperty<float>();
    
    [field: SerializeField] public float DetectRange { get; set; }
    [field: SerializeField][field: Range(0, 360)] public float ViewAngle { get; set; }

    private void Awake() => Init();
    
    private void Init()
    {
        Health.Value = _defaultHealth;
    }
}
