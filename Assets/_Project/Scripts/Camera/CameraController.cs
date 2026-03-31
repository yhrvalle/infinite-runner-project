using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
public class CameraController : MonoBehaviour
{

    [SerializeField] private float maxFOV = 85f;
    [SerializeField] private float minFOV = 35f;
    [SerializeField] private float zoomDuration = 1f;
    [SerializeField] private float fovChangeMultiplier = 1f;
    private CinemachineCamera _camera;

    private void Awake()
    {
        _camera = GetComponent<CinemachineCamera>();
    }
    public void ChangeCameraFOV(float moveSpeed)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeFOVRoutine(moveSpeed));
    }

    private IEnumerator ChangeFOVRoutine(float moveSpeed)
    {
        float startFOV = _camera.Lens.FieldOfView;
        float targetFOV = Mathf.Clamp(startFOV + moveSpeed * fovChangeMultiplier, minFOV, maxFOV);
        float elapsedTime = 0f;

        while (elapsedTime < zoomDuration)
        {
            float t = elapsedTime / zoomDuration;
            elapsedTime += Time.deltaTime;
            _camera.Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, t);
            yield return null;
        }

        _camera.Lens.FieldOfView = targetFOV;

    }
}
