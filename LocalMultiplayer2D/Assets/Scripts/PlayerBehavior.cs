using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    private InputControls inputControl;

    private InputActionAsset inputActions;
    private InputActionMap playerActionMap;
    private InputAction moveAction;
    private InputAction dashAction;

    private Rigidbody2D rigibody;

    [Header("Movement Variables")]
    private Vector2 moveDirection;
    private bool dashed = false;
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private int velocity;
    [SerializeField] private int dashForce;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;

    private void Awake()
    {
        inputControl = GameManager.instance.inputManager.inputControls;
        inputActions = GetComponent<PlayerInput>().actions;
        playerActionMap = inputActions.FindActionMap("Player");
        moveAction = playerActionMap.FindAction("Walk");
        dashAction = playerActionMap.FindAction("Dash");

        rigibody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveHandler();
        DashHandler();
    }

    private void MoveHandler()
    {
        if (isDashing) return;
        moveDirection = moveAction.ReadValue<Vector2>() * velocity;
        rigibody.velocity = new Vector2(moveDirection.x * velocity, moveDirection.y * velocity);
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        //dashed = context.ReadValue<bool>();
        //dashed = context.action.triggered;

        if (canDash && context.performed)
        {
            DashCorotine();
        }
    }
         
    private void DashHandler()
    {
        if (canDash && dashed)
        {
            DashCorotine();
        }
    }

    private IEnumerator DashCorotine()
    {
        canDash = false;
        isDashing = true;
        rigibody.velocity = new Vector2(moveDirection.x * dashForce, moveDirection.y * dashForce);
        //trailRenderer.emitting = true;
        //animator.SetBool("isDashing", true);
        yield return new WaitForSeconds(dashTime);
        //isDashing = false;
        //trailRenderer.emitting = false;
        //animator.SetBool("isDashing", false);
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void OnEnable()
    {
        GameManager.instance.inputManager.EnablePlayerInput();
        inputControl.Player.Dash.performed += OnDash;
    }

    private void OnDisable()
    {
        GameManager.instance.inputManager.DisablePlayerInput();
        inputControl.Player.Dash.performed -= OnDash;
    }
}
