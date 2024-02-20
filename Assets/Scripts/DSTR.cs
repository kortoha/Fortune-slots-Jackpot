using System.Collections;
using UnityEngine;

public class DSTR : MonoBehaviour
{
    [SerializeField] private float _destroyTimer;

    private void OnEnable()
    {
        StartCoroutine(DestroySelf());
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(_destroyTimer);
        Destroy(gameObject);
    }
}
