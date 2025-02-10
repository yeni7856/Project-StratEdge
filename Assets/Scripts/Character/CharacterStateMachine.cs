using UnityEngine;

namespace MyStartEdge
{
    //�ִϸ��̼� ����
    public enum CharacterState 
    { 
        Idle, 
        Walking, 
        Shooting, 
        Attacking, 
        Die, 
        Win,
    }
    public class CharacterStateMachine : MonoBehaviour
    {
        #region Variabels
        public CharacterState currentState = CharacterState.Idle;

        private CharacterAIController characterController;
        private Health health;
        #endregion

        void Start()
        {
            characterController = GetComponent<CharacterAIController>();
            health = GetComponent<Health>();
            ChangeState(CharacterState.Idle);
        }

        void Update()
        {
            if(currentState == CharacterState.Die)
                return;
            if (health.IsDead)
            {
                ChangeState(CharacterState.Die);
                return;
            }
            switch (currentState)
            {
                case CharacterState.Idle:
                    characterController.LookForEnemy();
                break;
                case CharacterState.Walking:
                    characterController.MoveTowardsTarget();
                    break;
                case CharacterState.Shooting:
                    characterController.ShootTarget();
                    break;
                case CharacterState.Attacking:
                    characterController.AttackTarget();
                    break;
                case CharacterState.Win:
                    characterController.WaitBeforeNextFight();
                    break;
            }
        }

        //�ִϸ��̼� ���� ����
        public void ChangeState(CharacterState newState)
        {
            if (currentState == newState) return;
            currentState = newState;
            characterController.UpdateAnimation(newState);
        }
    }
}