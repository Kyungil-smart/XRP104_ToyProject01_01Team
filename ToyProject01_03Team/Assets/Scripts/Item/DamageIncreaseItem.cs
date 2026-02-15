using System;
using System.Collections;
using UnityEngine;

public class DamageIncreaseItem : MonoBehaviour
{
    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = FindAnyObjectByType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerController.isIncreasedDamage = true;
            gameObject.SetActive(false);
        }
    }
}

