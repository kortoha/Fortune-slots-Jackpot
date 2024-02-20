using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private int _coinsScore = 0;
    [SerializeField] private int _energyScore = 20;
    [SerializeField] private int _keysScore = 0;
    [SerializeField] private TextMeshProUGUI _energyScoreText;
    [SerializeField] private TextMeshProUGUI _coinScoreText;
    [SerializeField] private TextMeshProUGUI _keyScoreText;
    [SerializeField] private TextMeshProUGUI _keyPriceText;
    [SerializeField] private Image _energyBar;
    [SerializeField] private int _keyPrice;

    [SerializeField] private Animator _coinsAnimator;
    [SerializeField] private Animator _keysAnimator;

    [SerializeField] private GameObject _gameField;
    [SerializeField] private GameObject _minigameField;

    private bool _isMinigameOpen = false;

    private int _maxEnergyScore;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _maxEnergyScore = _energyScore;
    }

    private void Update()
    {
        UpdateVisual(_energyScore, _maxEnergyScore, _energyBar);

        if(_energyScore == 0 && !_isMinigameOpen)
        {
            Invoke("OpenMiniGame", 2f);
        }
    }

    private void UpdateVisual(int energyPoints, int maxEnergyScore, Image bar)
    {
        _energyScoreText.text = energyPoints.ToString() + " / " + maxEnergyScore.ToString();

        float healthLavel = (float)energyPoints / maxEnergyScore;

        bar.fillAmount = healthLavel;

        if (_energyScore < 0)
        {
            _energyScore = 0;
        }

        _keyPriceText.text = _keyPrice.ToString();
        _keyScoreText.text = _keysScore.ToString();
        _coinScoreText.text = _coinsScore.ToString();
    }

    public int GetMoney()
    {
        return _coinsScore;
    }

    public int GetEnergy()
    {
        return _energyScore;
    }

    public int GetKeys()
    {
        return _keysScore;
    }

    public void Buy(int amount)
    {
        if (_coinsScore >= amount)
        {
            SoundController.Instance.PlayBuySound();
            _coinsScore -= amount;
        }
    }
    public void UseKey(int amount)
    {
        if (_keysScore >= amount)
        {
            _keysScore -= amount;
        }
    }

    public void UseEnergy()
    {
        if (_energyScore > 0)
        {
            _energyScore--;
        }
    }

    public void LoseMoney(int amount)
    {
        if (_coinsScore >= amount)
        {
            _coinsScore -= amount;
        }
        else
        {
            _coinsScore = 0;
        }
    }

    public void WinEnergy(int amount)
    {
        _energyScore += amount;

        if (_energyScore > _maxEnergyScore)
        {
            _energyScore = _maxEnergyScore;
        }
    }

    public void WinKeys(int amount)
    {
        _keysScore += amount;
    }

    public void WinMoney(int amount)
    {
        _coinsScore += amount;
    }

    public void BuyKeys()
    {
        if (_coinsScore >= _keyPrice)
        {
            Buy(_keyPrice);
            _keysScore++;
        }
        else
        {  
            SoundController.Instance.PlayNoItemSound();
            
            _coinsAnimator.SetTrigger("NoMoney");
        }
    }

    public void NoKeysAnim()
    {
        _keysAnimator.SetTrigger("NoKeys");
    }

    private void OpenMiniGame()
    {
        _gameField.SetActive(false);
        _minigameField.SetActive(true);
        _isMinigameOpen = true;  
    }

    public void MiniGameState(bool isMiniGameActive)
    {
        _isMinigameOpen = isMiniGameActive;
    }

    public bool IsMiniGameOpen()
    {
        return _isMinigameOpen;
    }
}
