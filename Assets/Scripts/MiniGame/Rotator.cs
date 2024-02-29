using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 chipRotation;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Chip"))
        {
            other.transform.rotation = Quaternion.Euler(0, 0, chipRotation.z);
        }
    }
}
