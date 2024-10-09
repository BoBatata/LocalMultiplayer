using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    private PlayerInputs playerInputs;

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
        canDash = true;
        playerInputs = GetComponent<PlayerInputs>();

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
        moveDirection = playerInputs.move * velocity;
        rigibody.velocity = new Vector2(moveDirection.x * velocity, moveDirection.y * velocity);
    }
    public void DashHandler()
    {
        if (canDash && playerInputs.dash)
        {
            StartCoroutine(DashCorotine());
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
        isDashing = false;
        //trailRenderer.emitting = false;
        //animator.SetBool("isDashing", false);
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
