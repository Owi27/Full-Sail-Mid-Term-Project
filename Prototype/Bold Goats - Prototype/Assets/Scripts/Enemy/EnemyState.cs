using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{

    public class EnemyState : MonoBehaviour
    {
        public States state = States.Patrol;

        public delegate void StateChange();
        public event StateChange Alert;
        public event StateChange Investigate;
        public event StateChange Return;
        public event StateChange Patrol;
        public event StateChange Chase;
        public event StateChange Attack;

        public void InvokeAlert()
        {
            if (Alert != null)
            {
                Alert.Invoke();
            }

            ChangeState(States.Alert);
        }

        public void InvokeInvestigate()
        {
            SoundManager.PlaySound(SoundManager.Sound.EnemySpotPlayer);

            if (Investigate != null)
            {
                Investigate.Invoke();
            }

            ChangeState(States.Investigate);
        }

        public void InvokeReturn()
        {
            SoundManager.PlaySound(SoundManager.Sound.EnemyReturn);
            if (Return != null)
            {
                Return.Invoke();
            }

            ChangeState(States.Return);
        }

        public void InvokePatrol()
        {
            if (Patrol != null)
            {
                Patrol.Invoke();
            }

            ChangeState(States.Patrol);
        }

        public void InvokeChase()
        {
            SoundManager.PlaySound(SoundManager.Sound.EnemyChasePlayer);
            if (Chase != null) {
                Chase.Invoke();
            }

            ChangeState(States.Chase);
        }

        public void InvokeAttack()
        {
            SoundManager.PlaySound(SoundManager.Sound.EnemyAttack);
            if (Attack != null)
            {
                Attack.Invoke();
            }

            ChangeState(States.Attack);
        }

        public void InvokeDie()
        {

            ChangeState(States.Death);
        }

        public void Kill()
        {
            InvokeDie();
        }

        #region TEST CODE

        public bool ChangeToInvestigate;
        public bool ChangeToPatrol;
        public bool ChangeToReturn;
        public bool ChangeToChase;

        private void Update()
        {
            if (ChangeToInvestigate)
            {
                ChangeToInvestigate = false;
                InvokeInvestigate();
            }
            if (ChangeToPatrol)
            {
                ChangeToPatrol = false;
                InvokePatrol();
            }
            if (ChangeToChase)
            {
                ChangeToChase = false;
                InvokeChase();
            }
            if (ChangeToReturn)
            {
                ChangeToReturn = false;
                InvokeReturn();
            }
        }

        private void ChangeState(States _state)
        {
            state = _state;
            GetComponentInChildren<EnemyIndicator>().HandleOnStateChange(_state);
        }


        #endregion
    }

    public enum States
    {
        Patrol,
        Investigate,
        Alert,
        Chase,
        Attack,
        Return,
        Death
    }


}