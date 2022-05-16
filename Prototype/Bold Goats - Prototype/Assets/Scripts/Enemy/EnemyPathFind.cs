using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    [RequireComponent(typeof(EnemyVision))]
    public class EnemyPathFind : MonoBehaviour
    {
        private NavMeshAgent aiEnemy;
        public Transform[] guardPoints = null;
        private int destinationPoint = 0;

        public Color colorAttack;
        Color originalColor;

        Transform lastPosition;
        Transform investigatePosition;

        EnemyVision enemyVision;

        [HideInInspector]
        public bool lookingAround = false;

        //Delegate function to be called after the LookAround coroutine
        delegate void FunctionAfterLookingAround();

        EnemyState enemyState;

        [SerializeField] float maxDistanceForChase = 25f;
        [SerializeField] const float distanceToAttack = .5f;
        [SerializeField] Material attackMaterial;

        
        void Awake()
        {
            aiEnemy = GetComponent<NavMeshAgent>();
            enemyState = GetComponent<EnemyState>();
            enemyVision = GetComponent<EnemyVision>();
            originalColor = GetComponent<Renderer>().material.color;


            enemyState.Chase += HandleInvokeChase;
            enemyState.Investigate += HandleInvokeInvestigate;
            enemyState.Return += HandleInvokeReturn;
            enemyState.Attack += HandleInvokeAttack;

            lastPosition = new GameObject().transform;
            investigatePosition = new GameObject().transform;

            lastPosition.name = gameObject.name + " lastPos";
            investigatePosition.name = gameObject.name + " investigatePos";

            enemyState.state = States.Patrol;
            GoToNextPoint();
        }

        private void OnDisable()
        {
            enemyState.Chase -= HandleInvokeChase;
            enemyState.Investigate -= HandleInvokeInvestigate;
            enemyState.Return -= HandleInvokeReturn;
            enemyState.Attack -= HandleInvokeAttack;
        }

        // Update is called once per frame
        void Update()
        {

            PerformNavigation();
        }

        void GoToNextPoint()
        {
            // If there are no points set, No Need to continue Function
            if (guardPoints.Length == 0)
            {
                return;
            }

            // Set Gaurd point to the point currently selected
            aiEnemy.destination = guardPoints[destinationPoint].position;

            // Set Destination Point to next point
            destinationPoint = (destinationPoint + 1) % guardPoints.Length;

        }

        public void HandleInvokeChase()
        {

        }

        public void HandleInvokeInvestigate()
        {
            lastPosition.position = transform.position;
            aiEnemy.destination = investigatePosition.position;
            investigatePosition = GameManager.Instance.Player.transform;
        }

        public void HandleInvokeReturn()
        {
            GetComponent<Renderer>().material.color = originalColor;
            aiEnemy.ResetPath();
            aiEnemy.destination = lastPosition.position;
        }

        public void HandleInvokeAttack()
        {
            GetComponent<Renderer>().material.color = colorAttack;
            SceneTransitionManager.Instance.LoadScene("LOSE CONDITION");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void HandleInvestigateLookAround()
        {
            enemyState.InvokeReturn();
        }

        public void ResumeNavigation()
        {
            aiEnemy.isStopped = false;
        }

        // Switch for enemy behavior
        void PerformNavigation()
        {
            switch (enemyState.state)
            {
                case States.Alert:

                    //Unused state

                    break;
                case States.Attack:

                    //Everything taken care of in HandleInvokeAttack

                    break;
                case States.Chase:

                    lookingAround = false;

                    Vector3 playerPos = GameManager.Instance.Player.transform.position;

                    aiEnemy.speed = 4.5f;
                    aiEnemy.destination = playerPos;

                    playerPos = new Vector3(playerPos.x, playerPos.y + 1, playerPos.z);

                    float distance = Vector3.Distance(playerPos, transform.position);

                    if (distance >= maxDistanceForChase)
                    {
                        enemyState.InvokeReturn();
                    }
                    else if (distance <= distanceToAttack)
                    {

                        aiEnemy.isStopped = true;
                        aiEnemy.ResetPath();
                        enemyState.InvokeAttack();
                    }


                    break;
                case States.Death:

                    aiEnemy.isStopped = true;
                    aiEnemy.ResetPath();

                    break;
                case States.Investigate:

                    if (aiEnemy.remainingDistance <= 1.0f && !aiEnemy.pathPending && !lookingAround)
                    {
                        lookingAround = true;
                        FunctionAfterLookingAround[] func = { HandleInvestigateLookAround , ResumeNavigation};
                        aiEnemy.isStopped = true;
                        StartCoroutine(LookAround(func, 5, 90));
                    }

                    break;
                case States.Patrol:

                    if (aiEnemy.remainingDistance < 1.0f && !aiEnemy.pathPending && !lookingAround )
                    {
                        lookingAround = true;
                        FunctionAfterLookingAround[] func = { GoToNextPoint, ResumeNavigation };
                        aiEnemy.isStopped = true;
                        StartCoroutine(LookAround(func, 3, 90, .25f));
                        
                    }

                    break;
                case States.Return:

                    lookingAround = false;
                    aiEnemy.speed = 3;
                    //Enemy has reached the last position
                    if (aiEnemy.remainingDistance <= 1.0f && !aiEnemy.pathPending)
                    {
                        //calculates which waypoint to set it to
                        destinationPoint--;
                        if (destinationPoint < 0)
                        {
                            destinationPoint = guardPoints.Length - 1;
                        }
                        //Returns patrolling
                        enemyState.InvokePatrol();
                    }

                    break;
                default:
                    break;
            }

        }
        public void SetInvestigatePosition(Transform position)
        {
            investigatePosition = position;
        }

        public void SetLastPosition(Transform position)
        {
            lastPosition = position;
        }
        IEnumerator LookAround(FunctionAfterLookingAround[] functionToCall, int timeToLook, float _angleToRotate, float timeBetweenRotations = 0)
        {
            //initialized variables
            Vector3 currentDir = transform.forward, originalDir = transform.forward;
            float angleToRotate = _angleToRotate;
            float degreesPerSecond = angleToRotate / ((float)timeToLook / 4f);

            //defines the vector that is rotated to the target
            Vector3 targetLeft = Quaternion.Euler(0, -angleToRotate, 0) * currentDir;
            Vector3 targetRight = Quaternion.Euler(0, angleToRotate, 0) * currentDir;

            //Loops until the angle is smaller than 5
            while (Mathf.Abs(Vector3.Angle(currentDir, targetLeft)) > 5)
            {
                //degrees per second based on the time to look

                //rotates and updates the dir
                transform.Rotate(new Vector3(0, -degreesPerSecond * Time.deltaTime, 0));
                currentDir = transform.forward;
                yield return null;
            }

            yield return new WaitForSeconds(timeBetweenRotations);

            //Do the same thing all the way right
            while (Mathf.Abs(Vector3.Angle(currentDir, targetRight)) > 5)
            {
                transform.Rotate(new Vector3(0, degreesPerSecond * Time.deltaTime, 0));
                currentDir = transform.forward;
                yield return null;
            }

            yield return new WaitForSeconds(timeBetweenRotations);

            //Return to center
            while (Mathf.Abs(Vector3.Angle(currentDir, originalDir)) > 5)
            {
                transform.Rotate(new Vector3(0, -degreesPerSecond * Time.deltaTime, 0));
                currentDir = transform.forward;
                yield return null;
            }

            lookingAround = false;

            foreach (var function in functionToCall)
            {
                function();
            }
        }
    }
}
