using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    private readonly int _isMove = Animator.StringToHash("IsMove");
    private readonly int _isAim = Animator.StringToHash("IsAim");
    private readonly int _isFire = Animator.StringToHash("IsFire");
    private readonly int _isDie = Animator.StringToHash("IsDie");

    private Animator _animator;

    private void Awake() => Init();
    private void OnEnable() => SubscribeEvents();
    private void OnDisable() => UnsubscribeEvents();

    private void Init()
    {
        _animator = GetComponent<Animator>();
    }

    private void SubscribeEvents()
    {
    }

    private void UnsubscribeEvents()
    {
    }

    private void OnAnimMove(bool isMove) => _animator.SetBool(_isMove, isMove);
    private void OnAnimAim(bool isAim) => _animator.SetBool(_isAim, isAim);
    private void OnAnimFire() => _animator.SetTrigger(_isFire);
    private void OnAnimDie(bool isDie) => _animator.SetBool(_isDie, isDie);
}
