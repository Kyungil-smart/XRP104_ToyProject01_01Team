using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private Button _titleButton;
    [SerializeField] private Button _continueButton;

    private void Awake() => Init();
    private void OnEnable() => SubscribeEvents();
    private void Start() => Deactivate();
    private void OnDisable() => UnsubscribeEvents();
    private void OnDestroy() => Dispoase();

    private void Init()
    {
        GameManager.Instance.OnGamePause += Activate;
    }

    private void SubscribeEvents()
    {
        _titleButton.onClick.AddListener(ToTitle);
        _continueButton.onClick.AddListener(Continue);
    }

    private void UnsubscribeEvents()
    {
        _titleButton.onClick.RemoveListener(ToTitle);
        _continueButton.onClick.RemoveListener(Continue);
    }

    private void Activate()
    {
        gameObject.SetActive(true);
        
        RefreshElapsedTimeText();
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void RefreshElapsedTimeText()
    {
        _timeText.text = $"Elapsed Time : {StageInfo.Instance.ElapsedTime.ToString("0.00")} sec";
    }

    private void ToTitle()
    {
        SceneManager.LoadScene(0);
    }

    private void Continue()
    {
        GameManager.Instance.GameResume();
        gameObject.SetActive(false);
    }

    private void Dispoase()
    {
        GameManager.Instance.OnGamePause -= Activate;
    }
}
