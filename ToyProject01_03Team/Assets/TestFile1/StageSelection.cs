using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelection : MonoBehaviour
{
    public void OnClickStage(StageInfoSO stageInfo)
    {
        GameManager.Instance.CurrentStage = stageInfo;
        SceneManager.LoadScene(2);
    }

    public void OnClickBack()
    {
        SceneManager.LoadScene(0);
    }
}
