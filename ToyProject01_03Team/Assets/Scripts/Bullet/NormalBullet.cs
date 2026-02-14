
using UnityEngine;

[RequireComponent(typeof(TrailRenderer),typeof(Rigidbody))]
public class NormalBullet : Bullet
{
    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;
    
    private void Awake() => Init();
    private void FixedUpdate() => CheckDistance();

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & HitLayer) == 0) return;
        OnHit(other);
    }
    
    public override void SetData(float speed, float power, float flightDistance, LayerMask hitLayer, Transform muzzlePoint)
    {
        Speed = speed;
        AttackPower = power;
        MuzzlePoint = muzzlePoint;
        HitLayer |= hitLayer;
        DeactivateDistance = flightDistance;
        
        transform.position = muzzlePoint.position;
        transform.forward = muzzlePoint.forward;
        _rigidbody.linearVelocity = transform.forward * Speed;
    }

    private void CheckDistance()
    {
        if (Vector3.Distance(transform.position, MuzzlePoint.position) <= DeactivateDistance) return;
        
        Destroy(gameObject);
    }

    private void Init()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    protected override void OnHit(Collider other)
    {
        IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
        if (damagable != null) damagable.TakeDamage(AttackPower);
        
        Destroy(gameObject);
    }
}
