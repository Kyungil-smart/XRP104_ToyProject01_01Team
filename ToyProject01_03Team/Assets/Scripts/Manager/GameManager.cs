using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int CurrentStage;
    
    private void Awake()
    {
        SingletonInit();
    }
}
