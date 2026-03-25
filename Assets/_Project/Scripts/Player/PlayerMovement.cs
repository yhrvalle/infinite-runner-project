using Enthalpy.Input;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInputReader reader;
    private Vector2 _movement;

    private void Start()
    {
        reader.EnablePlayerInputActions();
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
        Debug.Log(_movement);
    }
}
