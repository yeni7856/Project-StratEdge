using UnityEngine;
using UnityEngine.UI;

namespace MyStartEdge
{
    public class CharacterAIController : MonoBehaviour, IDamageable
    {
        #region Variables
        private Animator animator;
        [SerializeField] private float detectRange = 10f;    // �� ���� ����
        [SerializeField] private float attackRange = 2f;     // ���� ���� ����
        [SerializeField] private float moveSpeed = 3f;     // �̵� �ӵ�
        [SerializeField] private int health = 100;              //ü��
        [SerializeField] private Image healthBar;            //UI ü�¹�
        public LayerMask enemyLayer;        //��Layer
        public Transform firePoint;              //�Ѿ� ������
        public GameObject bulletPrefab;     //�Ѿ�


        private Transform target;                                                        // ���� Ÿ�� (�÷��̾ ��)
        private CharacterStateMachine characterStateMachine;      //�ʱ����
        private bool allEnemiesDefeated = false;                                //���� ���� �׾�����
        //[SerializeField] private string Enemytag = "Enemy";                 //Enemy �±�

        #endregion

        private void Start()
        {
            characterStateMachine = GetComponent<CharacterStateMachine>();
            animator = GetComponent<Animator>();
        }

        public void LookForEnemy()
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, detectRange, enemyLayer);
            if (enemies.Length > 0)
            {
                target = enemies[0].transform;
                characterStateMachine.ChangeState(CharacterState.Walking);
            }
        }

        public void MoveTowardsTarget()
        {
            if (target == null) return;

            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= attackRange)
            {
                characterStateMachine.ChangeState(CharacterState.Attacking);
                return;
            }
            else if (distance <= detectRange)
            {
                characterStateMachine.ChangeState(CharacterState.Shooting);
            }

            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            transform.LookAt(target);
        }

        public void ShootTarget()
        {
            if (target == null) return;

            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            characterStateMachine.ChangeState(CharacterState.Idle);
        }

        public void AttackTarget()
        {
            if (target == null) return;
            animator.SetTrigger("IsAttacking");
        }

        public void PlayVictoryAnimation()
        {
            animator.SetBool("AllEnemiesDefeated", true);
        }

        public void WaitBeforeNextFight()
        {
            Invoke(nameof(EndPostBattleIdle), 3f);
        }

        private void EndPostBattleIdle()
        {
            characterStateMachine.ChangeState(CharacterState.Idle);
        }

        public void UpdateAnimation(CharacterState newState)
        {
            animator.SetBool("IsShooting", newState == CharacterState.Shooting);
            animator.SetBool("IsWalking", newState == CharacterState.Walking);
            animator.SetBool("IsAttacking", newState == CharacterState.Attacking);
            animator.SetBool("IsIdle", newState == CharacterState.Idle);
        }

        public void TakeDamage(float damage)
        {
            
        }
    }
 }
