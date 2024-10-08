using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerBehavior : MonoBehaviour
{
    private InputControls inputControls;

    private Rigidbody2D rigibody;

    [Header("Movement Variables")]
    private Vector2 moveDirection;
    [SerializeField] private int velocity;

    private void Awake()
    {
        rigibody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        inputControls = GameManager.instance.inputManager.inputControls;
    }

    private void Update()
    {
        MoveHandler();
    }

    private void MoveHandler()
    {
        moveDirection = inputControls.Movement.Walk.ReadValue<Vector2>();
        rigibody.velocity = new Vector2(moveDirection.x * velocity, moveDirection.y * velocity).normalized;
    }
}
