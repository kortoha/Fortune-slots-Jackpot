using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    [Header("Arrays")]
    [SerializeField] private GameObject[] _levelPages;
    [SerializeField] private GameObject[] _tutorialPages;

    [Header("LevelButtons")]
    [SerializeField] private Button[] _buttons;
    [Header("UI Panels")]

    [SerializeField] private GameObject _start;
    [SerializeField] private GameObject _levels;
    [SerializeField] private GameObject _tutorials;

    private int currentLevelIndex = 0;
    private int currentTutorialPageIndex = 0;

    private void Start()
    {
        SetNumbersOnLevels();
    }

    private void SetNumbersOnLevels()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            int id = i + 1;
            GetButtonText(_buttons[i], id);
        }
    }

    private void GetButtonText(Button btn, int id)
    {
        TextMeshProUGUI btnsText = btn.GetComponentInChildren<TextMeshProUGUI>();

        if (btnsText != null)
        {
            btnsText.text = id.ToString();
        }
    }

    public void OpenLevels()
    {
        SoundController.Instance.PlayClickSound();
        _start.SetActive(false);
        _levels.SetActive(true);
    }

    public void OpenTutorials()
    {
        SoundController.Instance.PlayClickSound();
        _tutorials.SetActive(true);
        _start.SetActive(false);
    }

    public void LevelsNext()
    {
        SoundController.Instance.PlayClickSound();
        if (currentLevelIndex < _levelPages.Length - 1)
        {
            _levelPages[currentLevelIndex].SetActive(false);
            currentLevelIndex++;
            _levelPages[currentLevelIndex].SetActive(true);
        }
    }

    public void LevelsPrevious()
    {
        SoundController.Instance.PlayClickSound();
        if (currentLevelIndex > 0)
        {
            _levelPages[currentLevelIndex].SetActive(false);
            currentLevelIndex--;
            _levelPages[currentLevelIndex].SetActive(true);
        }
    }
    public void CloseTutorial()
    {
        SoundController.Instance.PlayClickSound();
        _tutorials.SetActive(false);
        _start.SetActive(true);
    }

    public void CloseLevel()
    {
        SoundController.Instance.PlayClickSound();
        _levels.SetActive(false);
        _start.SetActive(true);
    }
    public void TutorialPrevious()
    {
        SoundController.Instance.PlayClickSound();
        if (currentTutorialPageIndex > 0)
        {
            _tutorialPages[currentTutorialPageIndex].SetActive(false);
            currentTutorialPageIndex--;
            _tutorialPages[currentTutorialPageIndex].SetActive(true);
        }
    }

    public void TutorialNext()
    {
        SoundController.Instance.PlayClickSound();
        if (currentTutorialPageIndex < _tutorialPages.Length - 1)
        {
            _tutorialPages[currentTutorialPageIndex].SetActive(false);
            currentTutorialPageIndex++;
            _tutorialPages[currentTutorialPageIndex].SetActive(true);
        }
    }

    public void PlayLevel(int levelIndex)
    {
        SoundController.Instance.PlayClickSound();
        SceneManager.LoadScene(levelIndex);
    }
}
