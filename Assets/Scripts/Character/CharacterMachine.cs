using UnityEngine;

namespace MyStartEdge
{
    //애니메이션 상태
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
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void ChangeState(CharacterState newState)
        {
            if (currentState == newState) return;
            currentState = newState;
            UpdateAnimation(newState);

            switch (newState)
            {
                case CharacterState.Idle:
                    // Idle 상태
                    SetIdleAnimation();
                    break;
                case CharacterState.Walking:
                    // Walking 상태
                    SetWalkingAnimation();
                    break;
                case CharacterState.Shooting:
                    // Shooting 상태
                    SetShootingAnimation();
                    break;
                case CharacterState.Attacking:
                    // Attacking 상태 
                    SetAttackingAnimation();
                    break;
                case CharacterState.Dead:
                    // Dead 상태
                    SetDeadAnimation();
                    break;
                case CharacterState.Win:
                    // Win 상태 처리
                    SetWinAnimation();
                    // 승리시 추가 
                    break;
                default:
                    Debug.LogError("알 수 없는 상태: " + newState);
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
            animator.SetTrigger("IsAttacking"); // 공격 애니메이션 트리거
        }

        private void SetDeadAnimation()
        {
            animator.SetTrigger("IsDead");
        }

        private void SetWinAnimation()
        {
            animator.SetTrigger("Win");
        }

        // 애니메이션 상태 업데이트
        public void UpdateAnimation(CharacterState newState)
        {
            animator.SetBool("IsIdle", newState == CharacterState.Idle);
            animator.SetBool("IsWalking", newState == CharacterState.Walking);
            animator.SetBool("IsShooting", newState == CharacterState.Shooting);
            animator.SetBool("IsAttacking", newState == CharacterState.Attacking);
        }
    }
}
