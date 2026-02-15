using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelection : MonoBehaviour
{
    public void OnClickStage(int stage)
    {
        GameManager.Instance.CurrentStage = stage;
        SceneManager.LoadScene(2);
    }

    public void OnClickBack()
    {
        SceneManager.LoadScene(0);
    }
}
