using UnityEngine;

[CreateAssetMenu()]
public class SlotSO : ScriptableObject
{
    public enum Tipe
    {
        Fruits,
        Cards,
        Bar,
        Stuf
    }

    public Tipe tipe;
    public Sprite sprite;
    public int amount;

    public int index;

    public GameObject fx;
}
