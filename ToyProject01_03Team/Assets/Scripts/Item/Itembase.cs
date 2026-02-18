using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Itembase : MonoBehaviour
{
    [SerializeField] private Vector2 _randomForceXZ;
    [SerializeField] private float _forceY;
    protected Rigidbody Rigidbody;
    
    public void Drop()
    {
        if (Rigidbody == null) return;
        
        float x = Random.Range(-_randomForceXZ.x, _randomForceXZ.x);
        float z = Random.Range(-_randomForceXZ.y, _randomForceXZ.y);
        Rigidbody.AddForce(new Vector3(x, 0, z), ForceMode.Impulse);
    }
}
