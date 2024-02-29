using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private GameObject _fx;
    [SerializeField] private GameObject[] _stufArrau;

    private SpriteRenderer _spriteRenderer;
    private Collider _collider;

    private void OnEnable()
    {
        _collider = GetComponent<Collider>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Chip"))
        {
            if (!MGNew.Instance.isMove)
            {
                _spriteRenderer.enabled = false;
                Instantiate(_fx, gameObject.transform.position, Quaternion.identity);
                foreach (var item in _stufArrau)
                {
                    item.SetActive(false);
                }
                _collider.enabled = false;
                Destroy(other.gameObject);
            }
        }
    }

    private void OnDisable()
    {
        foreach (var item in _stufArrau)
        {
            item.SetActive(true);
        }
        _collider.enabled = true;
        _spriteRenderer.enabled = true;
    }
}
