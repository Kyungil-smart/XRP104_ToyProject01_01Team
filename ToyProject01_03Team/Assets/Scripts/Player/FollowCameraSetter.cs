using UnityEngine;
using UnityEngine.Serialization;

public class FollowCameraSetter : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField][Range(0, 90)] private float _angle;
    public bool _hasCameraControl { get; set; } = true;
    private Camera _camera;

    private void Awake() => Init();
    private void Reset() => DefaultSetup();
    
    private void Init()
    {
        _camera = Camera.main;
        
        _camera.orthographic = true;
        _camera.orthographicSize = 7;
    }
    
    private void DefaultSetup()
    {
        _offset.x = 0;
        _offset.y = 9;
        _offset.z = -7;
        _angle = 42.7f;
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
            _angle,
            _camera.transform.rotation.eulerAngles.y,
            _camera.transform.rotation.eulerAngles.z
        );
    }
}