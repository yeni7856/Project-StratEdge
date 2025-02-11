using UnityEngine;
using UnityEngine.UI;

namespace MyStartEdge
{
    public class CharacterAIController : MonoBehaviour, IDamageable
    {
        #region Variables
        private Animator animator;
        [SerializeField] private float detectRange = 10f;    // 적 감지 범위
        [SerializeField] private float attackRange = 2f;     // 근접 공격 범위
        [SerializeField] private float moveSpeed = 3f;     // 이동 속도
        [SerializeField] private int health = 100;              //체력
        [SerializeField] private Image healthBar;            //UI 체력바
        public LayerMask enemyLayer;        //적Layer
        public Transform firePoint;              //총알 포지션
        public GameObject bulletPrefab;     //총알


        private Transform target;                                                        // 현재 타겟 (플레이어나 적)
        private CharacterStateMachine characterStateMachine;      //초기상태
        private bool allEnemiesDefeated = false;                                //적이 전부 죽었는지
        //[SerializeField] private string Enemytag = "Enemy";                 //Enemy 태그

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
