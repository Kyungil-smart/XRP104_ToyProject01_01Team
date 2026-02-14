using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private float _defaultHealth;
    public ObservableProperty<float> Health = new ObservableProperty<float>();

    private void Awake() => Init();
    
    private void Init()
    {
        Health.Value = _defaultHealth;
    }
}
