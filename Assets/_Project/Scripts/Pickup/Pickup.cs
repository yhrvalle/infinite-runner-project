using UnityEngine;
public abstract class Pickup : MonoBehaviour
{
    private const string PlayerTag = "Player";

    [SerializeField] private float rotationSpeed = 100f;

    private void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(PlayerTag))
        {
            return;
        }

        OnPickup();
        Destroy(gameObject);
    }

    protected abstract void OnPickup();
}
