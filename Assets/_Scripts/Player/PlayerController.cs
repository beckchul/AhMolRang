using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //private InputActions input;

    public event Action<Vector2> OnMoveEvent;


    protected virtual void OnEnable()
    {
        //input = new InputActions();

        //input._2DSideScroller.Move.performed += OnMove;
        //input._2DSideScroller.Move.canceled += OnStopMove;

        //input.Enable();
    }

    protected virtual void OnDisable()
    {
        //input._2DSideScroller.Move.performed -= OnMove;
        //input._2DSideScroller.Move.canceled -= OnStopMove;

        //input.Disable();
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
