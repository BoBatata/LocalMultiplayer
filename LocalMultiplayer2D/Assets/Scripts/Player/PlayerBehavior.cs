using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    private PlayerInputs playerInputStates;
    private PlayerInput playerInput;

    private Rigidbody2D rigibody;
    private TrailRenderer trailRenderer;
    private Animator animator;

    [Header("Movement Variables")]
    private Vector2 moveDirection;
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private int velocity;
    [SerializeField] private int dashForce;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;

    [Header("Combat Variables")]
    [SerializeField] private bool canKill = false;

    private void Awake()
    {
        canDash = true;
        playerInputStates = GetComponent<PlayerInputs>();
        playerInput = GetComponent<PlayerInput>();

        rigibody = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        MoveHandler();
        DashHandler();
    }

    private void MoveHandler()
    {
        if (isDashing) return;
        moveDirection = playerInputStates.move * velocity;
        rigibody.velocity = new Vector2(moveDirection.x * velocity, moveDirection.y * velocity);

        if (moveDirection.x < 0)
        {
            transform.rotation = new Quaternion(0, -180, 0, 0);
        }
        else if (moveDirection.x > 0)
        {
            transform.rotation = quaternion.identity;
        }

        if (moveDirection != Vector2.zero)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
    public void DashHandler()
    {
        if (canDash && playerInputStates.dash)
        {
            StartCoroutine(DashCorotine());
        }
    }

    private IEnumerator DashCorotine()
    {
        canDash = false;
        isDashing = true;
        rigibody.velocity = new Vector2(moveDirection.x * dashForce, moveDirection.y * dashForce);
        trailRenderer.emitting = true;
        animator.SetTrigger("didDash");
        canKill = true;
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        canKill = false;
        trailRenderer.emitting = false;
        //animator.SetBool("isDashing", false);
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void DisableInputs()
    {
        playerInput.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Contains("Player") && canKill)
        {
            GameManager.instance.uiManager.EnableWinPanel(LayerMask.LayerToName(this.gameObject.layer));
            DisableInputs();
        }
    }   
}
