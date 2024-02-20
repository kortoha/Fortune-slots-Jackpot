using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public enum ChestState
    {
        Empty,
        HasEnergy,
        HasKey
    }

    [SerializeField] private Sprite _emptyChestSprite;
    [SerializeField] private Sprite _hasEnergySprite;
    [SerializeField] private Sprite _hasKeySprite;
    [SerializeField] private int _openPrice;

    [SerializeField] private WinItem _winKeyFX;
    [SerializeField] private WinItem _winEnergyFX;

    private ChestState _state;
    private Button _chestButton;
    private TextMeshProUGUI _chestOpenPriceText;

    private Image _chestImage;

    private bool _isClicked = false;

    private int _randomKeyCount;
    private int _randomEnergyCount;

    private void Start()
    {
        _chestImage = GetComponent<Image>();
        _chestButton = GetComponent<Button>();
        _chestOpenPriceText = GetComponentInChildren<TextMeshProUGUI>();
        _chestOpenPriceText.text = _openPrice.ToString();

        SetRandomChest();

        _chestButton.onClick.AddListener(() => { ChestAbility(); });

        _randomKeyCount = Random.Range(1, 5) * 10;
        _randomEnergyCount = Random.Range(1, 3) * 10;
    }

    private void SetRandomChest()
    {
        ChestState[] chestStates = (ChestState[])System.Enum.GetValues(typeof(ChestState));
        _state = chestStates[Random.Range(0, chestStates.Length)];
    }

    public void ChestAbility()
    {
        if (!_isClicked)
        {
            if(Player.Instance.GetKeys() >= _openPrice)
            {
                Player.Instance.UseKey(_openPrice);
                SoundController.Instance.PlayOpenChestSound();
                switch (_state)
                {
                    case ChestState.Empty:
                        _chestImage.sprite = _emptyChestSprite;
                        SoundController.Instance.PlayChestEmptySound();
                        break;
                    case ChestState.HasEnergy:
                        WinItem energyFX = Instantiate(_winEnergyFX, Vector2.zero, Quaternion.identity);
                        energyFX.winItemCount = _randomEnergyCount;
                        _chestImage.sprite = _hasEnergySprite;
                        Player.Instance.WinEnergy(_randomEnergyCount);
                        break;
                    case ChestState.HasKey:
                        WinItem keyFX = Instantiate(_winKeyFX, Vector2.zero, Quaternion.identity);
                        keyFX.winItemCount = _randomKeyCount;
                        _chestImage.sprite = _hasKeySprite;
                        Player.Instance.WinKeys(_randomKeyCount);
                        break;
                    default:
                        break;
                }
                _isClicked = true;
            }
            else
            {
                SoundController.Instance.PlayNoItemSound();
                Player.Instance.NoKeysAnim();
            }
        }
    }
    public bool IsClicked() 
    {  
        return _isClicked; 
    }
}
