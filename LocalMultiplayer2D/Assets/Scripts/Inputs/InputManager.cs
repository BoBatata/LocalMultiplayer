using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
    public InputControls inputControls;

    public InputManager()
    {
        inputControls = new InputControls();
        EnablePlayerInput();
    }

    public void EnablePlayerInput() => inputControls.Player.Enable();

    public void DisablePlayerInput() => inputControls.Player.Disable();
}
