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
        Win,
    }
    public class CharacterMachine : MonoBehaviour
    {
        private CharacterState currentState;
        private CharacterAIController characterController;
        private Animator animator;

        private void Awake()
        {
            characterController = GetComponent<CharacterAIController>();
            animator = GetComponent<Animator>();
        }

        public void ChangeState(CharacterState newState)
        {
            if (currentState == newState) return;
            currentState = newState;
            characterController.UpdateAnimation(newState);

            switch (newState)
            {
                case CharacterState.Idle:
                    // Idle ����
                    SetIdleAnimation();
                    break;
                case CharacterState.Walking:
                    // Walking ����
                    SetWalkingAnimation();
                    break;
                case CharacterState.Shooting:
                    // Shooting ����
                    SetShootingAnimation();
                    break;
                case CharacterState.Attacking:
                    // Attacking ���� 
                    SetAttackingAnimation();
                    break;
                case CharacterState.Dead:
                    // Dead ����
                    SetDeadAnimation();
                    break;
                case CharacterState.Win:
                    // Win ���� ó��
                    SetWinAnimation();
                    // �¸��� �߰� 
                    break;
                default:
                    Debug.LogError("�� �� ���� ����: " + newState);
                    break;
            }
        }
        private void SetIdleAnimation()
        {
            animator.SetBool("IsIdle", true);
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsShooting", false);
            animator.SetBool("IsAttacking", false);
        }

        private void SetWalkingAnimation()
        {
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsShooting", false);
            animator.SetBool("IsAttacking", false);
        }

        private void SetShootingAnimation()
        {
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsShooting", true);
            animator.SetBool("IsAttacking", false);
        }

        private void SetAttackingAnimation()
        {
            animator.SetTrigger("IsAttacking"); // ���� �ִϸ��̼� Ʈ����
        }

        private void SetDeadAnimation()
        {
            animator.SetTrigger("IsDead");
        }

        private void SetWinAnimation()
        {
            animator.SetTrigger("Win");
        }
    }
}
