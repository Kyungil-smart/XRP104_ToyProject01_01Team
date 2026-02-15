using UnityEngine;

public class SpeedUpItem : MonoBehaviour
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
            _playerController.isSpeedUp = true;
            gameObject.SetActive(false);
        }
    }
}
