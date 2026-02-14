using UnityEngine;

public class SceneSingleton<T> : MonoBehaviour where T : SceneSingleton<T>
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<T>();
            }
            return _instance;
        }
    }

    public void SceneSingletonInit()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(_instance.gameObject);
        }
        else
        {
            _instance = this as T;
        }
    }
}
