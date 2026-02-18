using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    Animator _animator;
    
    private bool _isDie;
    private PlayerTarget _playerTarget;

    private PlayerController _playerController;
    
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerTarget = GetComponent<PlayerTarget>();
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        float speed = Mathf.Abs(x) + Mathf.Abs(z);

        if(x == 0 && z == 0)
        speed = 0;
        
        _animator.SetFloat("MoveSpeed", speed);
        _animator.SetBool("isAim", _playerTarget.IsTargetEnemy && speed == 0 && !_isDie);

        if(_playerTarget.IsTargetEnemy && speed == 0 && !_isDie)
        {
            _animator.SetTrigger("Fire");
        }

        if(_playerController.playerCurrentHP <= 0 && !_isDie)
        {
            _isDie = true;
            _animator.SetBool("isDie", true);
            Destroy(gameObject, 1f);
            return;
        }
    }
    // private void OnEnable()
    // {
    //     if(_playerController.playerCurrentHP <= 0 && !_isDie)
    //     {
    //         _isDie = true;
    //         _animator.SetBool("isDie", true);
    //         Destroy(gameObject, 2f);
    //     }
    // }

}
