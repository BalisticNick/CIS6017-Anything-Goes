using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using SceneSystem;
using Datapack;
using Collectibles;
using UnityEngine.SceneManagement;

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

        private Vector3 resetPoint;
        private Quaternion rot;

        //Player Speed (Editible in Editor)
        [SerializeField] private float speed = 90;

        //Interaction
        private bool canInteract = false;
        private bool canOpen = false;

        string sceneName = string.Empty;


        #endregion

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            playerInput = new PlayerInput();

            rot = Quaternion.Euler(90f, 0, 0);

            Scene currentScene = SceneManager.GetActiveScene();
            sceneName = currentScene.name;

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
            resetPoint = new Vector3(rb.position.x, 0, rb.position.z);


            //Movement
            movementContex = context;

            playerVeloctiy = Vector2.zero;
            playerVeloctiy = movement.ReadValue<Vector2>();
            playerVeloctiy.Normalize();

            rb.AddForce(playerVeloctiy.x * speed * Time.deltaTime, 0, playerVeloctiy.y * speed * Time.deltaTime);

            if (rb.position.y >= 0.12f)
            {
                StartCoroutine(PlayerGrabber());
            }

        }

        IEnumerator PlayerGrabber()
        {
            rb.velocity = Vector3.zero;
            rb.transform.SetPositionAndRotation(resetPoint, rot);
            yield return new WaitUntil(() => rb.position.y <= 0.11f);
            StopAllCoroutines();

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

            if (sceneName == "Warehouse 1")
            {
                canInteract = Datapack.DatapackTracker.canInteract;
            }
            else if (sceneName == "Challenge 1")
            {
                canOpen = CollectibleManager.isOpen;
            }
            else
            {
                canInteract = false;
                canOpen = false;
            }


            if (interact.triggered && canInteract)
            {
                Debug.Log("hallo");
                SceneManager.LoadScene("Challenge 1");

                canInteract = false;
            }

            if (interact.triggered && canOpen)
            {
                UI_Timer TimerUI = FindAnyObjectByType<UI_Timer>();
                bool hiscore = false;
                float timeRemaining = TimerUI.timer;
                float timeTaken = 120f - timeRemaining;

                string minutes = Mathf.Floor(timeTaken / 60).ToString("0");
                string seconds = Mathf.Floor(timeTaken % 60).ToString("00");

                if (PlayerPrefs.GetFloat("hiscoreTime", Mathf.Infinity) > timeTaken)
                {
                    PlayerPrefs.SetFloat("hiscoreTime", timeTaken);
                    hiscore = true;
                }
                if (hiscore)
                    SceneDirector.WinGame($"Congratulations you set a new hiscore of {minutes}:{seconds}!");
                else
                    SceneDirector.WinGame($"You took {minutes}:{seconds} to complete the challenge!");
                canOpen = false;
            }
        }

        #endregion

        private void FixedUpdate()
        {
            onMove(movementContex);


        }

    }
}