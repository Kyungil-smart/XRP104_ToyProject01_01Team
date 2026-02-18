using System;
using UnityEngine;

public class HealItem : Itembase
{
    [Header("아이템 획득 시 회복량")]
    [SerializeField] private float _healAmount;
    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = FindAnyObjectByType<PlayerController>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Healing");
            _playerController.playerCurrentHP += _healAmount;
            if (_playerController.playerCurrentHP > _playerController.playerMaxHP)
            {
                _playerController.playerCurrentHP = _playerController.playerMaxHP;
            }
            
            Destroy(gameObject);
            // gameObject.SetActive(false);
        }
    }
    
    
}
