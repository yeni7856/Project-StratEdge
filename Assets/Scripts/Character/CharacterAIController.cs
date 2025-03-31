using UnityEngine;
using UnityEngine.UI;

namespace MyStartEdge
{
    public class CharacterAIController : MonoBehaviour, IDamageable
    {
        #region Variables
        private Animator animator;
        private Transform detectedTarget;  //감지 된 적의 위치 저장
        private CharacterMachine characterMachine;      //캐릭터 상태
        private Health health;

        [Header("AI Settings")]
        [SerializeField] private float detectRange = 10f;    // 적 감지 범위
        [SerializeField] private float attackRange = 2f;     // 근접 공격 범위
        [SerializeField] private float moveSpeed = 3f;     // 이동 속도

        [Header("Shooting Settings")]
        public Transform firePoint;              //총알 포지션
        public GameObject bulletPrefab;     //총알 프리팹

        private bool isWin = false;            // 적이 전부 죽었는지 확인
        private bool isDragging = false;     //드래그여부
        private Transform startParent;

        private CharacterData characterData;     //캐릭터 데이터
        #endregion

        private void Start()
        {
            characterMachine = GetComponent<CharacterMachine>();
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();
        }

        private void FixedUpdate()
        {
            //플레이어 덱에 있을경우 Idle 상태 고정
            if (transform.parent != null && transform.parent.GetComponent<PlayerSpawnTile>() != null)
            {
                characterMachine.ChangeState(CharacterState.Idle);
                return;
            }

            LookForEnemy();

            if (isWin)
            {
                Debug.Log("승리!");
                //승리시 골드추가
                //다음 스테이지 준비
            }
        }
        private void OnMouseDown()
        {
            isDragging = true;
            startParent = transform.parent; // 드래그 시작 전 부모 저장
            Debug.Log("캐릭터 터치됨");
        }

        private void OnMouseDrag()
        {
            if (!isDragging)
                return;

            //마우스 월드 좌표 변환
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.transform.position.y;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = new Vector3(mousePos.x, transform.position.y, mousePos.z);
        }

        //마우스 타일위에 
        private void OnMouseUp()
        {
            isDragging = false;
            // 타일에 배치 시도
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                Tile tile = hit.collider.GetComponent<Tile>();
                if (tile != null && tile.IsPlaceable())
                {
                    transform.SetParent(tile.transform); // 타일의 자식으로 설정
                    transform.position = tile.transform.position;
                }
                else
                {
                    transform.SetParent(startParent); // 원래 부모로 복귀
                }
            }
            else
            {
                transform.SetParent(startParent); // 원래 부모로 복귀
            }
        }

        // 적 감지 및 공격 또는 이동
        public void LookForEnemy()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            Debug.Log("감지된 적의 수: " + enemies.Length);
            if (enemies.Length > 0)
            {
                //가까운 적 찾기
                GameObject closestEnemy = FindClosestEnemy(enemies);
                if (closestEnemy != null)
                {
                    detectedTarget = closestEnemy.transform;    //적위치저장
                    Debug.Log("감지된 적 위치: " + detectedTarget.position);

                    MoveOrShoot(); // 이동 또는 사격 결정
                }
            }
            else
            {
                detectedTarget = null;
                CheckAllEnemiesDefeated();
                return;
            }
        }

        //적 감지
        private GameObject FindClosestEnemy(GameObject[] enemies)
        {
            GameObject closestEnemy = null;
            float closestDistance = Mathf.Infinity;  //최소거리
            foreach (GameObject enemy in enemies)
            {
                if (enemy == null)
                {
                    Debug.LogWarning("적 오브젝트가 null입니다.");
                    continue; // null인 경우 건너뛰기
                }
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }
            return closestEnemy;
        }

        //이동 또는 공격 결정(타일위에 있을때만 실행)
        public void MoveOrShoot()
        {
            Debug.Log("MoveOrShoot 호출");
            //캐릭터가 타일 위에 없으면 움직이지 않음
            if(transform.parent == null || transform.parent.GetComponent<Tile>() == null)
            {
                return;
            }

            if (detectedTarget == null) return;

            float distance = Vector3.Distance(transform.position, detectedTarget.position);
            if (distance <= detectRange)
            {
                Debug.Log("사격 범위 내");
                ShootTarget(); // 사격 범위 내에 있으면 사격
                return;
            }
            else if (distance <= attackRange)    //근접시 Attack
            {
                Debug.Log("근접공격 범위 내");
                characterMachine.ChangeState(CharacterState.Attacking);
                return;
            }
            else
            {
                Debug.Log("사격 범위 밖");
                characterMachine.ChangeState(CharacterState.Walking); // 사격 범위 밖에 있으면 이동
            }

            //적쪽으로 포지션
            Vector3 direction = (detectedTarget.position - transform.position).normalized;
            //y축 사용 안함
            direction.y = 0;
            // 앞으로 이동
            transform.position += direction * moveSpeed * Time.deltaTime;
            // 타겟 방향으로 회전 (Y축만 회전)
            if(direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
            }
        }

        //슛팅 총알 발사
        public void ShootTarget()
        {
            characterMachine.ChangeState(CharacterState.Shooting);
            if(bulletPrefab != null && firePoint != null)
            {
                GameObject bulletGo= Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Destroy(bulletGo, 2f);
            }
        }

        // 적이 있는지 확인
        public void CheckAllEnemiesDefeated()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length > 0)
            {
                return; // 적이 있으면 종료
            }
            isWin = true;       //타일 위에 적 없으면 승리 처리
            characterMachine.ChangeState(CharacterState.Win);
        }
        
        //적 감지 범위
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
                health.Die();
            }
        }

        //캐릭터 데이터
        public void SetCharacterData(CharacterData data)
        {
            characterData = data;
            detectRange = data.detectRange;
            attackRange = data.attackRange;
            moveSpeed = data.moveSpeed; 
        }
    }
 }
