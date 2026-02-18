using System;
using System.Collections;
using UnityEngine;

public class PlayerBullet : MonoBehaviour, IShootable
{
    [Header("총알 속도")]
    [SerializeField] private float _bulletSpeed;
    [Header("총알 유지 시간")]
    [SerializeField] private float _bulletLifeTime;
    [Header("총알 프리펩")]
    [SerializeField] GameObject _playerBulletPrefab;

    private float _bulletDamage;
    private Rigidbody _rigidbody;
    private PlayerController _playerController;
    private EnemyController _enemyController;
    private WaitForSeconds bulletLifetime;

    private void Awake()
    {
       Init();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("Hit");
            PlayerBulletManager.Instance.DespawnBullet(this);
            _bulletDamage = _playerController.playerDamage;
            _enemyController.TakeDamage(_bulletDamage);
        }
    }

    private void Init()
    {
        _playerController = FindAnyObjectByType<PlayerController>();
        _enemyController = FindAnyObjectByType<EnemyController>();
        _rigidbody = GetComponent<Rigidbody>();
        bulletLifetime = new WaitForSeconds(_bulletLifeTime);
    }

    public void OnSpawn()
    {
        _bulletDamage = _playerController.playerDamage;
        _rigidbody.linearVelocity = transform.forward * _bulletSpeed;
        
        StartCoroutine(bulletCoroutine());
    }

    public void OnDespawn()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        StopCoroutine(bulletCoroutine());
    }

    IEnumerator bulletCoroutine()
    {
        yield return bulletLifetime;
        PlayerBulletManager.Instance.DespawnBullet(this);
    }
}
