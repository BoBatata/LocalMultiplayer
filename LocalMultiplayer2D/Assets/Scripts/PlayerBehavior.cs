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

    private Rigidbody2D rigibody;

    [Header("Movement Variables")]
    private Vector2 moveDirection;
    [SerializeField] private int velocity;

    private void Awake()
    {
        rigibody = GetComponent<Rigidbody2D>();

        inputAsset = GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
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
        moveDirection = move.ReadValue<Vector2>() * velocity;
        rigibody.velocity = new Vector2(moveDirection.x * velocity, moveDirection.y * velocity).normalized;
    }

    private void OnEnable()
    {
        move = player.FindAction("Walk");
    }

    private void OnDisable()
    {
        player.Disable();
    }
}
