using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace FOV
{
    public class FieldOfView : MonoBehaviour
    {
        #region "Fov Varibles"
        [Header("FOV Varibles")]
        public float radius;
        [Range(0, 360)]
        public float angle;

        public GameObject playerRef;

        public LayerMask targetMask;
        public LayerMask obstructionMask;

        public bool canSeePlayer;
        #endregion

        private void Start()
        {
            //fov start
            playerRef = GameObject.FindGameObjectWithTag("Player");
            StartCoroutine(FOVRoutine());

        }

        #region "Field Of View"
        private IEnumerator FOVRoutine()
        {
            WaitForSeconds wait = new WaitForSeconds(0.2f);

            while (true)
            {
                yield return wait;

                canSeePlayer = FieldOfViewCheck();
            }
        }

        private bool FieldOfViewCheck()
        {
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

            if (rangeChecks.Length != 0)
            {
                Transform target = rangeChecks[0].transform;
                Vector3 directionToTarget = (target.position - transform.position);

                if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2) //changed from foward to right
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)) //changed to physics 2d raycast we are still using flat POV so the z shouldnt make a diffrence.
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }
        #endregion
    }
}