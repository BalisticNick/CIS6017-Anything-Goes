using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class inputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction movement;

    private InputAction.CallbackContext movementContex;

    [Header("Varibles")]
    private Rigidbody rb;
    private Vector2 playerVeloctiy;
    [SerializeField]private float speed = 90;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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

        playerVeloctiy = Vector2.zero;
        playerVeloctiy = movement.ReadValue<Vector2>();
        playerVeloctiy.Normalize();

        rb.AddForce(playerVeloctiy * speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        onMove(movementContex);
    }

}
