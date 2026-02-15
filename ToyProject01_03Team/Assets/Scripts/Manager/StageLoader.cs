using UnityEngine;

public class StageLoader : MonoBehaviour
{
    private void Awake()
    {
        int stageIndex = GameManager.Instance.CurrentStage;
        DataManager.Instance.LoadStageInfo(stageIndex);
    }
}
