using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class MarbleInput : MonoBehaviour
    {
        #region "Input & Context"
        private PlayerInput marbleInput;
        private InputAction gyro;

        private InputAction.CallbackContext gyroContext;
        #endregion

        #region "Marible Race Varibles"
        [SerializeField]private Rigidbody marbleRB; //The map for rotation
        private Vector3 gyroMovement;

        [SerializeField]private float rSpeed = 90f;
        #endregion

        private void Awake()
        {
            marbleInput = new PlayerInput();
            marbleRB = GetComponent<Rigidbody>();
        }

        #region "OnEnable & OnDisable"
        private void OnEnable()
        {
            gyro = marbleInput.MarbleRace.Rotation;

            marbleInput.Enable();
            gyro.performed += onGyro;
        }

        private void OnDisable()
        {
            marbleInput.Disable();
            gyro.performed -= onGyro;
        }
        #endregion

        #region "Marble Input"
        private void onGyro(InputAction.CallbackContext context)
        {
            gyroContext = context;
            // X Z for Gyro Movement

            gyroMovement = Vector3.zero;
            gyroMovement = gyro.ReadValue<Vector3>();
            gyroMovement.Normalize();

            //set range to stop the map from moving to far in a 

            marbleRB.AddForce(gyroMovement * rSpeed * Time.deltaTime);
            //gyro not working!!

            Debug.Log(gyroMovement);


        }
        #endregion
        private void FixedUpdate()
        {
            onGyro(gyroContext);
        }
    }

}
