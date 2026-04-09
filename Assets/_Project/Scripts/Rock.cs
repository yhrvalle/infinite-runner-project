using Unity.Cinemachine;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private float shakeIntensity = 10f;
    private CinemachineImpulseSource _impulseSource;
    private Camera _camera;

    private void Start()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();
        _camera = Camera.main;
    }

    private void OnCollisionEnter(Collision collision)
    {
        float distanceFromCamera = Vector3.Distance(transform.position, _camera.transform.position);
        float shake = (1f / distanceFromCamera) * shakeIntensity;
        shake = Mathf.Min(1f, shake);
        _impulseSource.GenerateImpulse(shake);
    }
}
