using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance { get; private set; }

    [SerializeField] private Image _image;
    [SerializeField] private Sprite _on;
    [SerializeField] private Sprite _off;

    private bool _isSoundMute = false;

    [SerializeField] private AudioClip _buy;
    [SerializeField] private AudioClip _chestEmpty;
    [SerializeField] private AudioClip _click;
    [SerializeField] private AudioClip _noItem;
    [SerializeField] private AudioClip _openChest;

    private void Awake()
    {
        Instance = this;

        _isSoundMute = PlayerPrefs.GetInt("Sound", 0) == 1;

        AudioListener.pause = _isSoundMute;

        UpdateVisual();
    }

    public void PlayBuySound()
    {
        AudioSource.PlayClipAtPoint(_buy, Camera.main.transform.position);
    }

    public void PlayChestEmptySound()
    {
        AudioSource.PlayClipAtPoint(_chestEmpty, Camera.main.transform.position);
    }

    public void PlayClickSound()
    {
        AudioSource.PlayClipAtPoint(_click, Camera.main.transform.position);
    }

    public void PlayNoItemSound()
    {
        AudioSource.PlayClipAtPoint(_noItem, Camera.main.transform.position);
    }

    public void PlayOpenChestSound()
    {
        AudioSource.PlayClipAtPoint(_openChest, Camera.main.transform.position);
    }

    private void UpdateVisual()
    {
        if (_image != null)
        {
            _image.sprite = _isSoundMute ? _off : _on;
        }
    }

    public void MuteOrUnMute()
    {
        _isSoundMute = !_isSoundMute;
        AudioListener.pause = _isSoundMute;

        UpdateVisual();

        PlayerPrefs.SetInt("Sound", _isSoundMute ? 1 : 0);
        PlayerPrefs.Save();
    }
}
