using UnityEngine;

public class ExitPortal : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;
    
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & _targetLayer.value) == 0) return;

        GameManager.Instance.StageClear();
    }
}
