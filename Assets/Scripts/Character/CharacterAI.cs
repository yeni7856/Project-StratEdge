using UnityEngine;

namespace MyStartEdge
{
    public class CharacterAI : MonoBehaviour
    {
        #region Variables
        private Animator animator;
        [SerializeField] private float detectRange = 10f; // �� ���� ����
        [SerializeField] private float attackRange = 2f;  // ���� ���� ����
        [SerializeField] private float moveSpeed = 3f;    // �̵� �ӵ�
        [SerializeField] private int health = 100;

        private Transform target; // ���� Ÿ�� (�÷��̾ ��)
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

            FindTarget(); // �� ã��
            HandleState(); // ���� ����
        }

        void FindTarget()
        {
            // ����ĳ��Ʈ�� �̿��� ���� �����մϴ�.
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, detectRange))
            {
                // �±װ� "Enemy"�� ��ü�� ����
                if (hit.transform.CompareTag(targetEnemy))
                {
                    target = hit.transform;  // Ÿ���� ã���� Ÿ�� ����
                }

                // �� �������� ȸ��
                LookAtTarget();
            }
            else
            {
                target = null;
                allEnemiesDefeated = true; // ���� ������ �¸� ó��
            }
        }

        void LookAtTarget()
        {
            if (target == null) return;

            // Ÿ�� ��ġ���� ���� ���
            Vector3 direction = target.position - transform.position;
            direction.y = 0; // y�� ȸ���� ������� ���� (3D������ �ʿ�� y�൵ ȸ����ų �� ����)

            // Ÿ�� �������� ȸ��
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f); // �ε巯�� ȸ��
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
                // ���� ����
                animator.SetBool("IsAttacking", true);
                animator.SetBool("IsShooting", false);
                animator.SetBool("IsWalking", false);
            }
            else if (distanceToTarget <= detectRange)
            {
                // �ָ��� ���� ��
                animator.SetBool("IsShooting", true);
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsAttacking", false);
            }
            else
            {
                // ������ �̵�
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
            Destroy(gameObject, 2f); // 2�� �� ����
        }

        void Win()
        {
            animator.SetTrigger("WinJump");
            Invoke("ResetToIdle", 3f); // 3�� �� Idle ����
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
            // ����ĳ��Ʈ ������ �ð������� Ȯ���� �� �ְ� �׷��ݴϴ�.
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * detectRange);
        }
    }
}
