using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _pausePanel;

    [SerializeField] private GameObject _game;

    [SerializeField] private int _scene;

    [SerializeField] private Chest[] _chestArray;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        bool allChestsClicked = true;

        for (int i = 0; i < _chestArray.Length; i++)
        {
            if (!_chestArray[i].IsClicked())
            {
                allChestsClicked = false;
                break; 
            }
        }

        if (allChestsClicked)
        {
            Invoke("WinGame", 2f);
        }
    }


    public void StopGame()
    {
        SoundController.Instance.PlayClickSound();
        if (!Player.Instance.IsMiniGameOpen())
        {
            _game.SetActive(false);
        }
        _gamePanel.SetActive(false);
        _pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        if (!Player.Instance.IsMiniGameOpen())
        {
            _game.SetActive(true);
        }
        _gamePanel.SetActive(true);
        _pausePanel.SetActive(false);
        Time.timeScale = 1;
        SoundController.Instance.PlayClickSound();
    }

    public void PlayAgain()
    {
        SoundController.Instance.PlayClickSound();
        Time.timeScale = 1;
        SceneManager.LoadScene(_scene);
    }

    public void BackToMenu()
    {
        SoundController.Instance.PlayClickSound();
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void NextLevel()
    {
        SoundController.Instance.PlayClickSound();
        _scene++;
        SceneManager.LoadScene(_scene);
    }

    public void NextLevelAfterLast()
    {
        SoundController.Instance.PlayClickSound();
        SceneManager.LoadScene(2);
    }

    public void WinGame()
    {
        _game.SetActive(false);
        _winPanel.SetActive(true);
        _gamePanel.SetActive(false);
    }
}
