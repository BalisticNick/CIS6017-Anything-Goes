using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FOV;

namespace AIController
{

    public class AiController : MonoBehaviour
    {
        [Header("Classes")]
        public FieldOfView fov;
        public NavMeshAgent navMeshAgent;

        [Header("Time & Rotate")]
        public float maxWaitTime;
        private float startWaitTime;

        private float timeToRotate;
        public float maxTimeToRotate;

        [Header("Speed")]
        public float speedWalk;
        public float speedRun;

        public Transform[] waypoints;
        int waypointIndex = 0;

        private Vector3 playerLasPos = Vector3.zero;
        private Vector3 playerPos;

        private bool playerInRange = false;
        private bool playerNear = false;
        private bool isPatrol = false;
        private bool hasCaughtPlayer = false;


        private void Start()
        {
            playerPos = Vector3.zero;
            isPatrol = true;

            maxWaitTime = startWaitTime;
            maxTimeToRotate = timeToRotate;

            navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = speedWalk;
            navMeshAgent.SetDestination(waypoints[waypointIndex].position);
        }

        #region "Movement"
        void Move(float speed)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = speed;
        }

        void StopMove()
        {
            navMeshAgent.isStopped = true;
            navMeshAgent.speed = 0;
        }
        #endregion

        #region "Waypoints"
        public void MovetoNextWaypoint()
        {
            waypointIndex = Random.Range(0, waypoints.Length);
            // random pick for next points

            //or we cam use
            //waypointIndex++;

            navMeshAgent.SetDestination(waypoints[waypointIndex].position);
        }

        public void MovetoWaypoint() => navMeshAgent.SetDestination(waypoints[waypointIndex].position);
        #endregion

        #region "Patrol System"

        private void Patroling()
        {
            if (playerNear)
            { 
                if (maxTimeToRotate <= 0)
                {
                    Move(speedWalk);
                    LookForPlayer(playerLasPos);
                }
                else
                {
                    StopMove();
                    maxTimeToRotate -= Time.deltaTime;
                }
            }
            else
            {
                playerInRange = false;
                playerLasPos = Vector3.zero;
                MovetoWaypoint();

                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                {
                    if (maxWaitTime <= 0)
                    {
                        MovetoNextWaypoint();
                        Move(speedWalk);

                        maxWaitTime = startWaitTime;
                    }
                    else
                    {
                        StopMove();
                        maxWaitTime -= Time.deltaTime;
                    }
                }
            }
        }

        private void OnPlayerView()
        {
            playerInRange = fov.canSeePlayer;

            if (playerInRange)
            {
                playerPos = fov.playerRef.transform.position;
            }

        }

        void LookForPlayer(Vector3 player)
        {
            navMeshAgent.SetDestination(player);
            if (Vector3.Distance(transform.position, player) <= 0.3)
            {
                if (maxWaitTime <= 0)
                {
                    playerNear = false;
                    Move(speedWalk);
                    MovetoWaypoint();

                    //waittime
                    maxWaitTime = startWaitTime;
                    maxTimeToRotate = timeToRotate;
                }
                else 
                {
                    StopMove();
                    maxWaitTime -= Time.deltaTime;  
                }
            }
        }

        private void Chasing()
        {
            playerNear = false;
            playerLasPos = Vector3.zero;

            if (!hasCaughtPlayer)
            {
                Move(speedRun);
                navMeshAgent.SetDestination(playerPos);
            }
            if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if(maxWaitTime <= 0 && !hasCaughtPlayer && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 6f) 
                { 
                    isPatrol = true;
                    hasCaughtPlayer = false;
                    Move(speedWalk);
                    maxTimeToRotate = timeToRotate;
                    maxWaitTime = startWaitTime;
                    MovetoWaypoint();
                }

                else
                {
                    if(Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 2.5f)
                    {
                        StopMove();
                        maxWaitTime -= Time.deltaTime;
                    }
                }
            }
        }

        void CaughtPlayer()=> hasCaughtPlayer = true;
        #endregion

        private void Update()
        {
            OnPlayerView();

            navMeshAgent.gameObject.transform.Rotate(90, 0, 0);

            if(!isPatrol)
            {
                Chasing();
            }
            else
            {
                Patroling();
            }

            if(hasCaughtPlayer)
            {
                SceneSystem.SceneDirector.LoseGame();
            }
        }

    }
}
