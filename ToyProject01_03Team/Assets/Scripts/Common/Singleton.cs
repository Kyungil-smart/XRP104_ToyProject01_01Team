using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<T>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    public void SingletonInit()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(_instance.gameObject);
        }
        else
        {
            _instance = this as T;
            DontDestroyOnLoad(_instance.gameObject);
        }
    }
}
