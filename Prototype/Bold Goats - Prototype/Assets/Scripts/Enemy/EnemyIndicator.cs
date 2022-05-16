using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Enemy
{
    public class EnemyIndicator : MonoBehaviour
    {
        EnemyState enemyState;
        Renderer rend;
        [SerializeField] Color[] colors;
        private void Awake()
        {
            rend = GetComponent<Renderer>();
            HandleOnStateChange(States.Patrol);
        }

        public void HandleOnStateChange(States _states)
        {
            switch (_states)
            {
                case States.Patrol:
                    rend.material.color = colors[0];
                    break;
                case States.Investigate:
                    rend.material.color = colors[1];
                    break;
                case States.Alert:
                    rend.material.color = colors[2];
                    break;
                case States.Chase:
                    rend.material.color = colors[3];
                    break;
                case States.Attack:
                    rend.material.color = colors[4];
                    break;
                case States.Return:
                    rend.material.color = colors[5];
                    break;
                case States.Death:
                    rend.material.color = colors[6];
                    break;
                default:
                    break;
            }
        }
    }
}
