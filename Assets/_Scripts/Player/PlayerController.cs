using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput input;
    private Player player;

    public event Action<Vector2> OnMoveEvent;


    protected virtual void OnEnable()
    {
        input = new PlayerInput();

        input.TopView.Move.performed += OnMove;
        input.TopView.Move.canceled += OnStopMove;

        input.Enable();
    }

    protected virtual void OnDisable()
    {
        input.TopView.Move.performed -= OnMove;
        input.TopView.Move.canceled -= OnStopMove;

        input.Disable();
    }

    public void Init(Player _player)
    {
        player = _player;
    }

    protected virtual void OnMove(InputAction.CallbackContext value)
    {
        Vector2 dir = value.ReadValue<Vector2>().normalized;

        OnMoveEvent?.Invoke(dir);
    }

    protected virtual void OnStopMove(InputAction.CallbackContext value)
    {
        OnMoveEvent?.Invoke(Vector2.zero);
    }
}
