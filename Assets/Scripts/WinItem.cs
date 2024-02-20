using System;
using TMPro;
using UnityEngine;

public class WinItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemCount;

    [NonSerialized] public int winItemCount;

    private void Update()
    {
        UpdateWinItemText();
    }

    private void UpdateWinItemText()
    {
        _itemCount.text = winItemCount.ToString();
    }
}
