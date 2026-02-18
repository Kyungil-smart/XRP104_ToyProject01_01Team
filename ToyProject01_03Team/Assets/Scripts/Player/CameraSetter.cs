using UnityEngine;

public class CameraSetter : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField][Range(0, 90)] private float _virtialAngleRange;
    public bool _hasCameraControl { get; set; } = true;
    private Camera _camera;

    private void Awake() => Init();
    
    private void Init()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        if(!_hasCameraControl) return;
        
        SetPosition();
        SetRotateX();
    }

    private void SetPosition()
    {
        _camera.transform.position = transform.position + _offset;
    }

    private void SetRotateX()
    {
        _camera.transform.rotation = Quaternion.Euler(
            _virtialAngleRange,
            _camera.transform.rotation.eulerAngles.y,
            _camera.transform.rotation.eulerAngles.z
            );
    }
}
