using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] private Image _bar;

    private float _time;

    private void Awake()
    {
        _time = Random.Range(4, 6);

        StartCoroutine(Loader());
    }

    private IEnumerator Loader()
    {
        float timer = 0f;

        while (timer < _time)
        {
            timer += Time.deltaTime;
            float fillAmount = Mathf.Clamp01(timer / _time);
            _bar.fillAmount = fillAmount;
            yield return null;
        }
        _bar.fillAmount = 1f;
        SceneManager.LoadScene(1);
    }
}
