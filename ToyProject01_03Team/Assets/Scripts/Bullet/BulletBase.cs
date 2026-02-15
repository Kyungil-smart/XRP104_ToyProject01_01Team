using UnityEngine;
using UnityEngine.Serialization;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected LayerMask HitLayer;
    protected float DeactivateDistance;
    protected float Speed;
    protected Transform MuzzlePoint;
    protected float AttackPower;

    public abstract void SetData(float speed, float power, float flightDistance, LayerMask hitLayer,
        Transform muzzlePoint);
    protected abstract void OnHit(Collider other);
}
