using Unity.Cinemachine;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private ParticleSystem collisionParticles;
    [SerializeField] private AudioSource collisionAudioSource;
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
        CameraShake();
        CollisionParticles(collision);
        PlayCollisionSound();
    }
    private void CameraShake()
    {

        float distanceFromCamera = Vector3.Distance(transform.position, _camera.transform.position);
        float shake = (1f / distanceFromCamera) * shakeIntensity;
        shake = Mathf.Min(1f, shake);
        _impulseSource.GenerateImpulse(shake);
    }
    
    private void CollisionParticles(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0];
        collisionParticles.transform.position = contactPoint.point;
        collisionParticles.Play();
    }
    
    private void PlayCollisionSound()
    {
        collisionAudioSource.Play();
    }
}
