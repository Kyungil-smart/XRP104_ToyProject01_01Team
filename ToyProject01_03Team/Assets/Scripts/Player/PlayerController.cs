using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour, IDamagable
{
    [Header("플레이어 체력")]
    [SerializeField] public float playerMaxHP;
    public float playerCurrentHP;

    [Header("플레이어 데미지")] 
    [SerializeField] public float playerDamage;

    [Header("공격 쿨타임")] 
    [SerializeField] public float _attackCD;

    [Header("적용 가능한 버프들")]
    [SerializeField] private GameObject[] buffs;

    [Header("버프 제어 목록")] 
    [SerializeField] private int _buffTime;
    [SerializeField] private float _increasedDamage;
    [SerializeField]  private float _speedUp;
    
    [Header("총구 위치")]
    [SerializeField] private GameObject _muzzlePoint;
    [Header("총알 프리펩")]
    [SerializeField] private GameObject _playerBulletPrefab;
    [FormerlySerializedAs("_bulletManager")] [SerializeField] private PlayerBulletManager _bulletManagerPrefab;
    
    
    public bool isIncreasedDamage = false;
    public bool isSpeedUp = false;
    [FormerlySerializedAs("isCanAttack")] public bool CanAttack;
    private WaitForSeconds buffTime;
    private WaitForSeconds AttackCD;
    private PlayerMovement _playerMovement;
    private PlayerTarget _playerTarget;
    

    public event Action OnFire;
    public event Action OnDeath;
    public bool IsDead;

    public bool HasPlayerControl { get; private set; }

    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += ActivatePlayerControl;
        GameManager.Instance.OnGameOver += DeactivatePlayerControl;
        GameManager.Instance.OnGamePause += DeactivatePlayerControl;
        GameManager.Instance.OnGameResume += ActivatePlayerControl;
        GameManager.Instance.OnStageClear += DeactivatePlayerControl;
    }

    private void Update()
    {
        if (!HasPlayerControl) return;
        
        GetBuffs();
        
        // if(움직임이 없고,적이 락온 된 상태라면) 어택
        if (IsCanAttackState())
        {
            Attack();
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= ActivatePlayerControl;
        GameManager.Instance.OnGameOver -= DeactivatePlayerControl;
        GameManager.Instance.OnGamePause -= DeactivatePlayerControl;
        GameManager.Instance.OnGameResume -= ActivatePlayerControl;
        GameManager.Instance.OnStageClear += DeactivatePlayerControl;
    }

    private void ActivatePlayerControl()
    {
        HasPlayerControl = true;
    }

    private void DeactivatePlayerControl()
    {
        HasPlayerControl = false;
    }

    private bool IsCanAttackState()
    {
        return !_playerMovement.IsMoving && _playerTarget.IsDetectedEnemy;
    }

    private void Attack()
    {
        if (!CanAttack) return;
        
        transform.rotation = Quaternion.LookRotation(_playerTarget.Target.transform.position- transform.position);
        Vector3 bulletPos = GetPos();
        Quaternion bulletRot = GetRot(); 
        PlayerBulletManager.Instance.ShootBullet(bulletPos, bulletRot);
        OnFire?.Invoke();

        StartCoroutine(AttackCDCoroutine());
        CanAttack = false;
    }
    
    Vector3 GetPos()
    {
        return _muzzlePoint.transform.position;
    }
    
    Quaternion GetRot()
    {
        return _muzzlePoint.transform.rotation;
    }

    private void GetBuffs()
    {
        foreach (var buff in buffs) 
        {
            if (isIncreasedDamage && buff.CompareTag("DamageBuffs")) 
            {
                buff.SetActive(true);
                playerDamage += _increasedDamage;
                StartCoroutine(DamageBuffTimeCoroutine(buff));
                isIncreasedDamage = false;
                break; 
            }
            
            else if (isSpeedUp && buff.CompareTag("SpeedBuff"))
            {
                buff.SetActive(true);
                _playerMovement._moveSpeed += _speedUp;
                StartCoroutine(SpeedBuffTimeCoroutine(buff));
                isSpeedUp = false;
                break;
            }
        }
    }

    private void Init()
    {
        playerCurrentHP = playerMaxHP;
        _playerMovement = GetComponent<PlayerMovement>();
        _playerTarget = GetComponentInChildren<PlayerTarget>();
        buffTime = new WaitForSeconds(_buffTime);
        AttackCD = new WaitForSeconds(_attackCD);
        CanAttack = true;
        IsDead = false;

        if (PlayerBulletManager.Instance == null)
        {
            Instantiate(_bulletManagerPrefab);
        }
    }

    public void TakeDamage(float damage)
    {
        playerCurrentHP -= damage;

        if (playerCurrentHP <= 0) Die();
    }

    IEnumerator AttackCDCoroutine()
    {
        yield return AttackCD;
        CanAttack = true;
    }

    IEnumerator DamageBuffTimeCoroutine(GameObject buffObject)
    {
        yield return buffTime;

        playerDamage -= _increasedDamage;
        buffObject.SetActive(false);
    }
    IEnumerator SpeedBuffTimeCoroutine(GameObject buffObject)
    {
        yield return buffTime;

        _playerMovement._moveSpeed -= _speedUp;
        buffObject.SetActive(false);
    }
    
    private void Die()
    {
        if(IsDead) return;
        
        IsDead = true;
        OnDeath?.Invoke();
        GameManager.Instance.GameOver();
    }
}
