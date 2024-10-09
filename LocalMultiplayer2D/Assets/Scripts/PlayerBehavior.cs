using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    private InputControls inputControls;

    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction move;
    private InputAction dash;

    private Rigidbody2D rigibody;

    [Header("Movement Variables")]
    private Vector2 moveDirection;
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private int velocity;
    [SerializeField] private int dashForce;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;

    private void Awake()
    {
        rigibody = GetComponent<Rigidbody2D>();

        inputAsset = GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
    }


    private void Start()
    {
        inputControls = GameManager.instance.inputManager.inputControls;
        //GameManager.instance.inputManager.Dash += DashHandler;
    }

    private void Update()
    {
        MoveHandler();
    }

    private void MoveHandler()
    {
        if (isDashing) return;
        moveDirection = move.ReadValue<Vector2>() * velocity;
        rigibody.velocity = new Vector2(moveDirection.x * velocity, moveDirection.y * velocity);
    }

    private void DashHandler(InputAction.CallbackContext obj)
    {
        if (canDash && obj.performed)
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
        move = player.FindAction("Walk");
        player.FindAction("Dash").started += DashHandler;
        player.Enable();
    }

    private void OnDisable()
    {
        player.FindAction("Dash").started -= DashHandler;
        player.Disable();
    }
}
