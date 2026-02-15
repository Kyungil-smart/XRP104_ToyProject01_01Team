using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletManager : SceneSingleton<PlayerBulletManager>
{
    [SerializeField] GameObject _bulletPrefab;

    [SerializeField] private int _defaultSize;
    
    // Dictionary<GameObject,Queue<GameObject>> bullets = new Dictionary<GameObject,Queue<GameObject>>();
    private Stack<PlayerBullet> _pool;
    
    
    void Awake()
    {
        Init();
        SceneSingletonInit();
    }

    private void Init()
    {
        _pool= new Stack<PlayerBullet>(_defaultSize);
    }

    private void ShootBullet(Transform muzzle)
    {
        PlayerBullet bullet;
        
        if (_pool.Count == 0)   bullet = CreateBullet();
        else                    bullet = _pool.Pop();
        
        bullet.gameObject.SetActive(true);
        bullet.transform.position = muzzle.position;
        bullet.transform.rotation = muzzle.rotation;
        
        IShootable shootable = bullet as IShootable;
        shootable.OnSpawn();
    }

    private void DespawnBullet(PlayerBullet bullet)
    {
        _pool.Push(bullet);
        bullet.gameObject.SetActive(false);
        
        IShootable shootable = bullet as IShootable;
        shootable.OnDespawn();
    }

    private PlayerBullet CreateBullet()
    {
        // Bullet 생산
        // _pool.Push()
    }

    public GameObject ShootBullet(GameObject bulletPrefab, Vector3 bulletPos, Quaternion bulletRot)
    {
        if (!bullets.ContainsKey(bulletPrefab))
        {
            bullets.Add(bulletPrefab,new Queue<GameObject>());
        }

        GameObject bullet;

        if (bullets[bulletPrefab].Count > 0)
        {
            bullet = bullets[bulletPrefab].Dequeue();
        }
        else
        {
            bullet = Instantiate(bulletPrefab, transform);
            bullet.name = bulletPrefab.name;
        }

        bullet.transform.position = bulletPos;
        bullet.transform.rotation = bulletRot;
        bullet.SetActive(true);

        if (bullet.TryGetComponent(out IShootable shootable))
        {
            shootable.OnSpawn();
        }
        
        return bullet;
    }

    public void DespawnBullet(GameObject bulletPrefab, GameObject bullet)
    {
        if (!bullets.ContainsKey(bulletPrefab))
        {
            bullets.Add(bulletPrefab, new Queue<GameObject>());
        }

        if (bullet.TryGetComponent(out IShootable shootable))
        {
            shootable.OnDespawn();
        }
        
        bullet.SetActive(false);
        
        bullets[bulletPrefab].Enqueue(bullet);
    }
}
