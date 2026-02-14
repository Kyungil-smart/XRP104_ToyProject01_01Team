using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartEnd : MonoBehaviour
{
	public void OnClickGameStart()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickGameEnd()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}
