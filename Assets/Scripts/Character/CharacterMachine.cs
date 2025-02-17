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
        Dead,
    }
    public class CharacterMachine : MonoBehaviour
    {
        private CharacterState currentState;
        private CharacterAIController CharacterController;
        private Animator animator;

        private void Awake()
        {
            CharacterController = GetComponent<CharacterAIController>();
            animator = GetComponent<Animator>();
        }

        public void ChangeState(CharacterState newState)
        {
            if (currentState == newState) return;
            currentState = newState;
            CharacterController.UpdateAnimation(newState);
        }
    }
}
