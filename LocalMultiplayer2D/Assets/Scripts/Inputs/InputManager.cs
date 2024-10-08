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

    public void EnablePlayerInput() => inputControls.Player.Enable();

    public void DisablePlayerInput() => inputControls.Player.Disable();
}
