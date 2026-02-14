using UnityEngine;

[RequireComponent(typeof(EnemyStats))]
public class EnemyController : MonoBehaviour, IDamagable
{
    private EnemyStats _stats;

    private void Awake() => Init();

    private void Init()
    {
        _stats = GetComponent<EnemyStats>();
    }

    public void TakeDamage(float damage)
    {
        _stats.Health.Value -= damage;
    }
}
