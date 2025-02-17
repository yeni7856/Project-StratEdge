using UnityEngine;
using UnityEngine.UI;

namespace MyStartEdge
{
    public class CharacterAIController : MonoBehaviour, IDamageable
    {
        #region Variables
        private Animator animator;
        private Transform target;                                      // 현재 타겟 (플레이어나 적)
        private CharacterMachine characterMachine;      //초기상태
        private Health health;

        [Header("AI Settings")]
        [SerializeField] private float detectRange = 10f;    // 적 감지 범위
        [SerializeField] private float attackRange = 2f;     // 근접 공격 범위
        [SerializeField] private float moveSpeed = 3f;     // 이동 속도
        public LayerMask enemyLayer;        //적Layer

        [Header("Shooting Settings")]
        public Transform firePoint;              //총알 포지션
        public GameObject bulletPrefab;     //총알 프리팹

        private bool allEnemiesDefeated = false; // 적이 전부 죽었는지 확인
        #endregion

        private void Start()
        {
            characterMachine = GetComponent<CharacterMachine>();
            animator = GetComponent<Animator>();
            health = GetComponent<Health>(); // ✅ Health 컴포넌트 가져오기
        }
        private void Update()
        {
            LookForEnemy();
        }

        // 적 감지 및 타겟 설정
        public void LookForEnemy()
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, detectRange, enemyLayer);
            if (enemies.Length > 0)
            {
                target = enemies[0].transform;
                characterMachine.ChangeState(CharacterState.Walking);
            }
            else
            {
                target = null;
                CheckAllEnemiesDefeated();
            }
        }

        // 이동 로직
        public void MoveTowardsTarget()
        {
            if (target == null) return;

            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= attackRange)
            {
                characterMachine.ChangeState(CharacterState.Attacking);
                return;
            }
            else if (distance <= detectRange)
            {
                characterMachine.ChangeState(CharacterState.Shooting);
            }

            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            transform.LookAt(target);
        }

        // 슈팅 로직
        public void ShootTarget()
        {
            if (target == null) return;

            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            characterMachine.ChangeState(CharacterState.Shooting);
        }

        // 공격 로직
        public void AttackTarget()
        {
            if (target == null) return;
            animator.SetTrigger("IsAttacking");
        }

        // 적이 전부 죽었는지 체크
        public void CheckAllEnemiesDefeated()
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, detectRange, enemyLayer);
            if (enemies.Length == 0)
            {
                allEnemiesDefeated = true;
                PlayVictoryAnimation();
            }
        }

        // 승리 애니메이션 실행
        public void PlayVictoryAnimation()
        {
            animator.SetBool("AllEnemiesDefeated", true);
        }

        // 애니메이션 상태 업데이트
        public void UpdateAnimation(CharacterState newState)
        {
            animator.SetBool("IsShooting", newState == CharacterState.Shooting);
            animator.SetBool("IsWalking", newState == CharacterState.Walking);
            animator.SetBool("IsAttacking", newState == CharacterState.Attacking);
            animator.SetBool("IsIdle", newState == CharacterState.Idle);
            animator.SetBool("AllEnemiesDefeated", allEnemiesDefeated);
        }

        // 디버깅용 기즈모 (적 감지 범위)
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectRange); // 적 감지 범위
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, attackRange); // 공격 범위
        }

        // 체력 감소 처리(Health 스크립트 활용)
        public void TakeDamage(float damage)
        {
            health.TakeDamage(damage);

            if (health.IsDead)
            {
                characterMachine.ChangeState(CharacterState.Dead);
            }
        }
    }
 }
