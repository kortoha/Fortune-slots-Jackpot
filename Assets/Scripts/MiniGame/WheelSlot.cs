using UnityEngine;

namespace MiniGame
{
    public class WheelSlot : MonoBehaviour
    {
        [SerializeField] private int _winEnergy;

        public void WinEnergy()
        {
            Player.Instance.WinEnergy(_winEnergy);
        }

        public int GetWinEnergyCount()
        {
            return _winEnergy;
        }
    }
}
