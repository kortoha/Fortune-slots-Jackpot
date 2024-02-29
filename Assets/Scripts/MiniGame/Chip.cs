using System;
using UnityEngine;

public class Chip : MonoBehaviour
{
    [NonSerialized] public bool isRelease = false;
    public GameObject arrow;

    private void Update()
    {
        if (isRelease)
        {
            Release();
        }
    }

    public void Release()
    {
        arrow.SetActive(false);
        Vector3 targetPosition = gameObject.transform.position + transform.up * 5f;
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetPosition, 3 * Time.deltaTime);

        Invoke("DestroySelf", 1);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
