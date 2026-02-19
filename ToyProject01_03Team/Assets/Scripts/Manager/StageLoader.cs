using UnityEngine;

public class StageLoader : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.CurrentStage.InitStage();
    }
}
