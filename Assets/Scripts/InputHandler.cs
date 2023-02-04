using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : Singleton<InputHandler>
{
    PlayerInput playerInput;

    public delegate void MovementPressedDelegate(float xAxis);
    public delegate void MousePositionDelegate(Vector2 mousePosition);
    public delegate void MousePressedDelegate(Vector2 mousePosition);

    public event MovementPressedDelegate OnMovementPressed;
    public event MousePositionDelegate OnMousePosition;
    public event MousePressedDelegate OnMousePressed;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = playerInput.actions["Movement"].ReadValue<Vector2>();

        if (direction != Vector2.zero)
            OnMovementPressed?.Invoke(direction.x);

        Vector2 mousePosition = playerInput.actions["MousePosition"].ReadValue<Vector2>();

        OnMousePosition?.Invoke(mousePosition);

        if (playerInput.actions["Shoot"].triggered)
            OnMousePressed?.Invoke(mousePosition);
    }
}
