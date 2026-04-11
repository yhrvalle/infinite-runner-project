using Enthalpy.Input;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInputReader reader;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float xClamp = 2f;
    [SerializeField] private float zClamp = 2f;

    private Vector2 _movement;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        reader.EnablePlayerInputActions();
    }
    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void OnEnable()
    {
        reader.Move += OnMove;
    }

    private void OnDisable()
    {
        reader.Move -= OnMove;
        reader.DisablePlayerInputActions();
    }
    private void OnMove(Vector2 direction)
    {
        _movement = direction;
    }

    private void HandleMovement()
    {
        Vector3 currentPosition = _rb.position;
        Vector3 playerDirection = new(_movement.x, 0f, _movement.y);
        Vector3 desiredPosition = currentPosition + playerDirection * (moveSpeed * Time.fixedDeltaTime);

        desiredPosition.x = Mathf.Clamp(desiredPosition.x, -xClamp, xClamp);
        desiredPosition.z = Mathf.Clamp(desiredPosition.z, -zClamp, zClamp);

        _rb.MovePosition(desiredPosition);
    }
    
    public void IncreaseMoveSpeed(float amount)
    {
        moveSpeed += amount;
    }
}
