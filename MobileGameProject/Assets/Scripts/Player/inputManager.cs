using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class inputManager : MonoBehaviour
{
    public PlayerInput playerInput;
    public InputAction movement;

    private InputAction.CallbackContext movementContex;

    [Header("Varibles")]
    private Rigidbody2D rb;
    private Vector2 v_Move;
    private float speed = 90;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        movement = playerInput.Player.Move;
        playerInput.Enable();
        movement.performed += onMove;

    }

    private void OnDisable()
    {
        playerInput.Disable();
        movement.performed -= onMove;
    }

    private void onMove(InputAction.CallbackContext context)
    {
        movementContex = context;
        v_Move = movement.ReadValue<Vector2>();
        rb.velocity = v_Move * speed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        onMove(movementContex);
    }

}
