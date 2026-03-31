using UnityEngine;
public class Pickup : MonoBehaviour
{
    private const string PlayerTag = "Player";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerTag))
        {
            Debug.Log("Picked up");
        }
    }
}
