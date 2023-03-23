using datapack;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using SceneSystem;

namespace Input
{
    public class inputManager : MonoBehaviour
    {
        #region "Input & Context"
        private PlayerInput playerInput;


        private InputAction movement;
        private InputAction interact;

        private InputAction.CallbackContext movementContex;
        private InputAction.CallbackContext interactContex;
        #endregion

        #region "Varibles"
        [Header("Movement")]
        private Rigidbody rb;
        private Vector2 playerVeloctiy;
        [SerializeField] private float speed = 90;
        private bool canInteract = false;
        #endregion

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            playerInput = new PlayerInput();

        }

        #region"OnEnable & OnDisable"
        private void OnEnable()
        {
            movement = playerInput.Player.Move;
            interact = playerInput.Player.Interact;
            playerInput.Enable();
            movement.performed += onMove;
            interact.performed += onInteract;

        }

        private void OnDisable()
        {
            playerInput.Disable();
            movement.performed -= onMove;
            interact.performed -= onInteract;
        }
        #endregion

        #region objectActions

        private void onMove(InputAction.CallbackContext context)
        {

            //-Movement
            movementContex = context;

            playerVeloctiy = Vector2.zero;
            playerVeloctiy = movement.ReadValue<Vector2>();
            playerVeloctiy.Normalize();

            rb.AddForce(playerVeloctiy * speed * Time.deltaTime);
        }

        private void onInteract(InputAction.CallbackContext context)
        {
            interactContex = context;

            datapackInteraction();
        }
        #endregion

        private void FixedUpdate()
        {
            onMove(movementContex);
        }

        #region UIInteractionCalls

        public void datapackInteraction()
        {
            canInteract = DatapackController.canInteract;

            if (interact.triggered && canInteract)
            {
                Debug.Log("hallo");
                SceneDirector.LoadRandomScene();
            }
        }

        #endregion

    }
}