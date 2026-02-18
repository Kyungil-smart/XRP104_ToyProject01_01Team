using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button _toTitleButton;

    private void Awake()        => Init();
    private void OnEnable()     => _toTitleButton.onClick.AddListener(ToTitle);
    private void Start()        => gameObject.SetActive(false);
    private void OnDisable()    => _toTitleButton.onClick.RemoveListener(ToTitle);
    private void OnDestroy()    => Dispose();

    private void Init()
    {
        GameManager.Instance.OnGameOver += Activate;
    }

    private void Dispose()
    {
        GameManager.Instance.OnGameOver -= Activate;
    }

    private void Activate()
    {
        gameObject.SetActive(true);
    }

    private void ToTitle()
    {
        SceneManager.LoadScene(0);
    }
}
