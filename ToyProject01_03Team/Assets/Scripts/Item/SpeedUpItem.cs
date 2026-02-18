using UnityEngine;

public class SpeedUpItem : Itembase
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
            _playerController.isSpeedUp = true;
            
            Destroy(gameObject);
            // gameObject.SetActive(false);
        }
    }
}
