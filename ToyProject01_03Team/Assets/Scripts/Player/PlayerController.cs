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

    [Header("적용 가능한 버프들")]
    [SerializeField] private GameObject[] buffs;

    [Header("버프 제어 목록")] 
    [SerializeField] private int _buffTime;
    [SerializeField] private float _increasedDamage;
    [SerializeField]  private float _speedUp;

    
    public bool isIncreasedDamage = false;
    public bool isSpeedUp = false;
    private WaitForSeconds buffTime;
    private PlayerMovement _playerMovement;
    
    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        GetBuffs();
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
        buffTime = new WaitForSeconds(_buffTime);
    }

    public void TakeDamage(float damage)
    {
        playerCurrentHP -= damage;
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
