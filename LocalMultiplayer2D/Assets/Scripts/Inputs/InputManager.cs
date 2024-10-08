using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public InputControls inputControls;
    public InputManager()
    {
        inputControls = new InputControls();
        EnablePlayerInput();
    }

    public void EnablePlayerInput() => inputControls.Movement.Enable();

    public void DisablePlayerInput() => inputControls.Movement.Disable();
}
