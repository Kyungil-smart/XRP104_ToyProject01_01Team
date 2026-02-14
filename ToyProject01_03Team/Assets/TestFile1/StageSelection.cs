using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelection : MonoBehaviour
{
    public void OnClickStage1()
    {
        SceneManager.LoadScene(2);
    }

    public void OnClickStage2()
    {
        SceneManager.LoadScene(2);
    }

    public void OnClickStage3()
    {
        SceneManager.LoadScene(2);
    }

    public void OnClickBack()
    {
        SceneManager.LoadScene(0);
    }
}
