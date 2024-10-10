using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
        [Header("Character Input Values")]
        public Vector2 move;
        public bool dash;

        public void OnWalk(InputValue value)
        {
            WalkInput(value.Get<Vector2>());
        }

        public void OnDash(InputValue value)
        {
            DashInput(value.isPressed); 
        }

        public void WalkInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void DashInput(bool newDashState)
        {
            dash = newDashState;
        }
}

