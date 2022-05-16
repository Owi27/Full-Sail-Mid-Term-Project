using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyState))]
    public class EnemyVision : MonoBehaviour
    {
        EnemyState enemyState;

        public float visionLength;
        [SerializeField] float stateChangeDelay = 1f;
        public float maxSightAngle = 30;

        float timeTillCanBeSeen;
        public bool checkForPlayer = false;

        private void Awake()
        {
            enemyState = GetComponent<EnemyState>();
        }

        private void Update()
        {
            //TODO Add angle checks
            if (checkForPlayer && GameManager.Instance.Player != null)
            {
                Vector3 dirToPlayer = GameManager.Instance.Player.transform.position - transform.position;

                dirToPlayer = new Vector3(dirToPlayer.x, dirToPlayer.y + 1, dirToPlayer.z);

                if (dirToPlayer.magnitude <= 2)
                {
                    enemyState.InvokeChase();
                    GetComponent<EnemyPathFind>().SetLastPosition(transform);
                    return;
                }
                if (Mathf.Abs(Vector3.Angle(transform.forward, dirToPlayer)) <= maxSightAngle/2)
                {
                    //Raycast to the player, certain distance. 
                    if (Physics.Raycast(transform.position, dirToPlayer, out RaycastHit hit, visionLength))
                    {
                        
                        //If we hit the player
                        if (hit.transform.gameObject == GameManager.Instance.Player)
                        {

                            if (enemyState.state == States.Patrol && timeTillCanBeSeen <= Time.time || enemyState.state == States.Return && timeTillCanBeSeen <= Time.time)
                            {
                                enemyState.state = States.Investigate;
                                EnemyPathFind enemy = GetComponent<EnemyPathFind>();
                                enemy.SetInvestigatePosition(GameManager.Instance.Player.transform);
                                enemy.StopAllCoroutines();
                                enemy.ResumeNavigation();
                                enemy.lookingAround = false;
                                timeTillCanBeSeen = Time.time + stateChangeDelay;
                                enemyState.InvokeInvestigate();
                            }
                            else if (enemyState.state == States.Investigate && Time.time >= timeTillCanBeSeen)
                            {
                                EnemyPathFind enemy = GetComponent<EnemyPathFind>();
                                enemy.StopAllCoroutines();
                                enemy.ResumeNavigation();
                                enemy.lookingAround = false;
                                enemyState.InvokeChase();
                            }
                           
                        }
                    }
                }
            }
        }




        private void OnDrawGizmosSelected()
        {
            GameObject player = GameObject.Find("Player");

            if (player != null)
            {
                Vector3 dirToPlayer = player.transform.position - transform.position;
                dirToPlayer.Normalize();
                Gizmos.color = Color.red;
                Gizmos.DrawRay(transform.position, dirToPlayer * visionLength);
            }

        }


    }
}
