using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
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

    
    public bool isIncreasedDamage = false;
    public bool isSpeedUp = false;
    public bool isCanAttack;
    private WaitForSeconds buffTime;
    private WaitForSeconds AttackCD;
    private PlayerMovement _playerMovement;
    private PlayerTarget _playerTarget;
    
    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        GetBuffs();
        
        // if(움직임이 없고,적이 락온 된 상태라면) 어택
        if (IsCanAttack())
        {
            Attack();
            StartCoroutine(AttackCDCoroutine());
            isCanAttack = false;
        }
    }

    private bool IsCanAttack()
    {
        if (!_playerMovement.isMoving && isCanAttack && _playerTarget.IsTargetEnemy)
        {
            Debug.Log("공격");
            return true;
        }
        else return false;
    }

    private void Attack()
    {
        Vector3 bulletPos = GetPos();
        Quaternion bulletRot = GetRot(); 
        PlayerBulletManager.Instance.ShootBullet(bulletPos, bulletRot);
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
        _playerTarget = FindFirstObjectByType<PlayerTarget>();
        buffTime = new WaitForSeconds(_buffTime);
        AttackCD = new WaitForSeconds(_attackCD);
        isCanAttack = true;
    }

    public void TakeDamage(float damage)
    {
        playerCurrentHP -= damage;
    }

    IEnumerator AttackCDCoroutine()
    {
        yield return AttackCD;
        isCanAttack = true;
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
}
