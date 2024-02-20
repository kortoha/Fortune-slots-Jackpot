using UnityEngine;

public class WheelIndicator : MonoBehaviour
{
    [SerializeField] private MiniGame _miniGame;
    [SerializeField] private GameObject _game;

    [SerializeField] private WinItem _winItem;

    private bool _isWinOnce = false;

    private void Update()
    {
        if(_miniGame.isWheelSpin)
        {
            _isWinOnce = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!_miniGame.isWheelSpin && !_miniGame.IsFirsSpin() && !_isWinOnce)
        {
            WheelSlot slot = collision.GetComponent<WheelSlot>();
            slot.WinEnergy();
            _isWinOnce = true;

            if(slot.GetWinEnergyCount() > 0)
            {
                WinItem winItem = Instantiate(_winItem, Vector2.zero, Quaternion.identity);
                winItem.winItemCount = slot.GetWinEnergyCount();
                Invoke("CloseMiniGame", 2f);
            }
            else
            {
                SoundController.Instance.PlayNoItemSound();
            }
        }
    }

    private void OnDisable()
    {
        Player.Instance.MiniGameState(false);
    }

    private void CloseMiniGame()
    {
        _game.SetActive(true);
        _miniGame.gameObject.SetActive(false);
    }
}
