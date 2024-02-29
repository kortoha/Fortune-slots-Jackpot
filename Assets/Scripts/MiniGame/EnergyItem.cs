using UnityEngine;

public class EnergyItem : MonoBehaviour
{
    [SerializeField] private WinItem _winItem;

    [SerializeField] private GameObject _miniGame;
    [SerializeField] private GameObject _game;
    [SerializeField] private GameObject[] _stufArrau;
    [SerializeField] private Transform[] _spawnPoints;

    private int _randomEnergyCount;
    private SpriteRenderer _spriteRenderer;

    private void OnEnable()
    {
        Transform newPos = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        gameObject.transform.position = newPos.position;
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _randomEnergyCount = Random.Range(5, 20);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Chip"))
        {
            _spriteRenderer.enabled = false;
            foreach (var item in _stufArrau)
            {
                item.SetActive(false);
            }
            WinItem winItem = Instantiate(_winItem, Vector2.zero, Quaternion.identity);
            winItem.winItemCount = _randomEnergyCount;
            Player.Instance.WinEnergy(_randomEnergyCount);
            Invoke("CloseMiniGame", 2);
        }
    }

    private void CloseMiniGame()
    {
        Player.Instance.MiniGameState(false);        
        _game.SetActive(true);
        _miniGame.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        foreach (var item in _stufArrau)
        {
            item.SetActive(true);
        }
        _spriteRenderer.enabled = true;
    }
}
