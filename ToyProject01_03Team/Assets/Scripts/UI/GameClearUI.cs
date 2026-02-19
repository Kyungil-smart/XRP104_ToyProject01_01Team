using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameClearUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _clearTime;
    [SerializeField] private Button _titleButton;
    
    private void Awake()        => Init();
    private void OnEnable()     => _titleButton.onClick.AddListener(ToTitle);
    private void Start()        => gameObject.SetActive(false);
    private void OnDisable()    => _titleButton.onClick.RemoveListener(ToTitle);
    private void OnDestroy()    => Dispose();
    
    private void Init()
    {
        GameManager.Instance.OnStageClear += Activate;
    }

    private void Dispose()
    {
        GameManager.Instance.OnStageClear -= Activate;
    }

    private void Activate()
    {
        gameObject.SetActive(true);
        RefreshClearTime();
    }

    public void RefreshClearTime()
    {
        _clearTime.text = $"Clear Time : {GameManager.Instance.ElapsedTime.ToString("0.00")} sec";
    }

    private void ToTitle()
    {
        SceneManager.LoadScene(0);
    }
}
