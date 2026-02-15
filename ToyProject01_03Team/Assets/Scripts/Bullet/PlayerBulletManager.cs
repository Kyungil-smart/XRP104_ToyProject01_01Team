using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletManager : MonoBehaviour
{
    [SerializeField] GameObject _bulletPrefab;
    Dictionary<GameObject,Queue<GameObject>> bullets = new Dictionary<GameObject,Queue<GameObject>>();
    
    public static PlayerBulletManager Instance;

    void Awake()
    {
        Instance = this;
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
