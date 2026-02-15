using System;
using UnityEngine;

public class HealItem : MonoBehaviour
{
    [Header("아이템 획득 시 회복량")]
    [SerializeField] private float _healAmount;
    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = FindAnyObjectByType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Healing");
            _playerController.playerCurrentHP += _healAmount;
            if (_playerController.playerCurrentHP > _playerController.playerMaxHP)
            {
                _playerController.playerCurrentHP = _playerController.playerMaxHP;
            }
            
            gameObject.SetActive(false);
        }
    }
    
    
}
