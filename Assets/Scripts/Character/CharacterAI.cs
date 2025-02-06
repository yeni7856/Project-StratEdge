using UnityEngine;

namespace MyStartEdge
{
    public class CharacterAI : MonoBehaviour
    {
        #region Variables
        private Animator animator;
        [SerializeField] private float detectRange = 10f; // 적 감지 범위
        [SerializeField] private float attackRange = 2f;  // 근접 공격 범위
        [SerializeField] private float moveSpeed = 3f;    // 이동 속도
        [SerializeField] private int health = 100;

        private Transform target; // 현재 타겟 (플레이어나 적)
        private bool allEnemiesDefeated = false;
        private string targetEnemy = "Enemy";
        #endregion

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (health <= 0)
            {
                Die();
                return;
            }

            if (allEnemiesDefeated)
            {
                Win();
                return;
            }

            FindTarget(); // 적 찾기
            HandleState(); // 상태 관리
        }

        void FindTarget()
        {
            // 레이캐스트를 이용해 적을 감지합니다.
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, detectRange))
            {
                // 태그가 "Enemy"인 객체만 감지
                if (hit.transform.CompareTag(targetEnemy))
                {
                    target = hit.transform;  // 타겟을 찾으면 타겟 설정
                }

                // 적 방향으로 회전
                LookAtTarget();
            }
            else
            {
                target = null;
                allEnemiesDefeated = true; // 적이 없으면 승리 처리
            }
        }

        void LookAtTarget()
        {
            if (target == null) return;

            // 타겟 위치와의 방향 계산
            Vector3 direction = target.position - transform.position;
            direction.y = 0; // y축 회전은 고려하지 않음 (3D에서는 필요시 y축도 회전시킬 수 있음)

            // 타겟 방향으로 회전
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f); // 부드러운 회전
        }

        void HandleState()
        {
            if (target == null)
            {
                animator.SetBool("IsShooting", false);
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsAttacking", false);
                return;
            }

            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget <= attackRange)
            {
                // 근접 공격
                animator.SetBool("IsAttacking", true);
                animator.SetBool("IsShooting", false);
                animator.SetBool("IsWalking", false);
            }
            else if (distanceToTarget <= detectRange)
            {
                // 멀리서 총을 쏨
                animator.SetBool("IsShooting", true);
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsAttacking", false);
            }
            else
            {
                // 적에게 이동
                animator.SetBool("IsWalking", true);
                animator.SetBool("IsShooting", false);
                animator.SetBool("IsAttacking", false);
                MoveTowardsTarget();
            }
        }

        void MoveTowardsTarget()
        {
            if (target != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            }
        }

        void Die()
        {
            animator.SetTrigger("Die");
            Destroy(gameObject, 2f); // 2초 후 삭제
        }

        void Win()
        {
            animator.SetTrigger("WinJump");
            Invoke("ResetToIdle", 3f); // 3초 후 Idle 복귀
        }

        void ResetToIdle()
        {
            animator.SetTrigger("Idle");
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }

        private void OnDrawGizmos()
        {
            // 레이캐스트 범위를 시각적으로 확인할 수 있게 그려줍니다.
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * detectRange);
        }
    }
}
