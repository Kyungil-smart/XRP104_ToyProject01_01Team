using System;
using System.Collections;
using UnityEngine;

public class DamageIncreaseItem : Itembase
{
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
            _playerController.isIncreasedDamage = true;
            Destroy(gameObject);
            // gameObject.SetActive(false);
        }
    }
}

