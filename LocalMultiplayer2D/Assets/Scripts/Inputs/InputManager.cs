using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
    public InputControls inputControls;

    public event Action Dash;

    public InputManager()
    {
        inputControls = new InputControls();
        EnablePlayerInput();

        inputControls.Player.Dash.performed += OnDash;
    }

    private void OnDash(InputAction.CallbackContext context) => Dash?.Invoke();

    public void EnablePlayerInput() => inputControls.Player.Enable();

    public void DisablePlayerInput() => inputControls.Player.Disable();
}
