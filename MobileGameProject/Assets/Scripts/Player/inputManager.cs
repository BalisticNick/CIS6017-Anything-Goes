using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using SceneSystem;
using Datapack;

namespace MobileInput
{
    public class inputManager : MonoBehaviour
    {
        #region "Input & Context"
        //Call Player Input Class
        private PlayerInput playerInput;

        //Input Action
        private InputAction movement;
        private InputAction interact;

        //Context
        private InputAction.CallbackContext movementContex;
        private InputAction.CallbackContext interactContex;
        #endregion

        #region "Player Varibles"
        [Header("Movement")]
        private Rigidbody rb;

        //Vector Movement
        private Vector2 playerVeloctiy;

        //Player Speed (Editible in Editor)
        [SerializeField] private float speed = 90;

        //Interaction
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

        #region "Player Movement"
        //player movement for Main Level

        private void onMove(InputAction.CallbackContext context)
        {

            //Movement
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

        #region "UIInteractionCalls"

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

        private void FixedUpdate()
        {
            onMove(movementContex);
        }

    }
}