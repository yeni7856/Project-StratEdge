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

        private CharacterData characterData;     //캐릭터 데이터

        [Header("AI Settings")]
        [SerializeField] private float detectRange = 10f;    // 적 감지 범위
        [SerializeField] private float attackRange = 2f;     // 근접 공격 범위
        [SerializeField] private float moveSpeed = 3f;     // 이동 속도

        [Header("Shooting Settings")]
        public Transform firePoint;              //총알 포지션
        public GameObject bulletPrefab;     //총알 프리팹

        private bool isDragging = false;     //드래그여부
        private Transform startParent;          //프리팹 부모
        private bool isBattleStarted = false;  //전투 시작 여부

        private Tile currentHoveredTile;

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
            if (!isBattleStarted)
            {
                characterMachine.ChangeState(CharacterState.Idle);
                return;
            }
            //전투 시작후 적감지 실행
            LookForEnemy();
        }

        public void ForceStartBattle()
        {
            isBattleStarted = true;
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
            transform.position = new Vector3(worldPos.x, transform.position.y, worldPos.z);

            // Raycast로 타일 감지
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, 100f);

            Tile foundTile = null;
            foreach (var hit in hits)
            {
                Tile tile = hit.collider.GetComponent<Tile>();
                if (tile != null)
                {
                    foundTile = tile;
                    break;
                }
            }

            // 이전 타일 하이라이트 해제
            if (currentHoveredTile != null && currentHoveredTile != foundTile)
                currentHoveredTile.Highlight(false);

            // 새 타일 하이라이트
            if (foundTile != null)
            {
                bool isSwapTarget = false;
                CharacterAIController other = foundTile.GetComponentInChildren<CharacterAIController>();

                // 다른 캐릭터가 타일 위에 있는 경우만 flash 처리
                if (other != null && other != this && foundTile.transform != startParent)
                {
                    isSwapTarget = true;
                }

                foundTile.Highlight(true, isSwapTarget);
                currentHoveredTile = foundTile;
            }
        }

        //마우스 타일위에 
        private void OnMouseUp()
        {
            isDragging = false;

            // 하이라이트 해제
            if (currentHoveredTile != null)
            {
                currentHoveredTile.Highlight(false);
                currentHoveredTile = null;
            }

            // 타일에 배치 시도
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray,100f);
            foreach(var hit in hits)
            {
                Transform tileTransform = null;
                CharacterAIController targetCharacter = null;

                // 타일 타입 감지
                if (hit.collider.TryGetComponent(out Tile tile))
                {
                    tileTransform = tile.transform;
                    targetCharacter = tile.GetComponentInChildren<CharacterAIController>();
                }
                else if (hit.collider.TryGetComponent(out PlayerSpawnTile deckTile))
                {
                    tileTransform = deckTile.transform;
                    targetCharacter = deckTile.GetComponentInChildren<CharacterAIController>();
                }

                if (tileTransform == null) continue;

                // 스왑
                if (targetCharacter != null && targetCharacter != this && tileTransform != startParent)
                {
                    Transform myTile = transform.parent;
                    Transform targetTile = targetCharacter.transform.parent;

                    transform.SetParent(targetTile);
                    transform.position = targetTile.position;

                    targetCharacter.transform.SetParent(myTile);
                    targetCharacter.transform.position = myTile.position;

                    if (myTile.TryGetComponent(out Tile myTileScript))
                        StartCoroutine(myTileScript.Flash());
                    if (targetTile.TryGetComponent(out Tile targetTileScript))
                        StartCoroutine(targetTileScript.Flash());

                    return;
                }

                // 빈 자리 or 자기 자리로 배치
                bool isPlaceable = (targetCharacter == null) || (tileTransform == startParent);

                if (isPlaceable)
                {
                    transform.SetParent(tileTransform);
                    transform.position = tileTransform.position;

                    if (tileTransform.TryGetComponent(out Tile t))
                        StartCoroutine(t.Flash());

                    return;
                }
                //Tile tile = hit.collider.GetComponent<Tile>();
                //if (tile == null) continue;

                ////해당 타일 위에 다른캐릭터가 있으면 서로 자리 바꿈
                //CharacterAIController target = tile.GetComponentInChildren<CharacterAIController>();
                //if(target != null && target != this)
                //{
                //    //현재 캐릭터, 타겟 캐릭터 타일 기억
                //    Transform myTile = this.transform.parent;
                //    Transform targetTile = target.transform.parent;

                //    //위치 바꾸기
                //    this.transform.SetParent(targetTile);
                //    this.transform.position = targetTile.position;

                //    target.transform.SetParent(myTile);
                //    target.transform.position = myTile.position;

                //    //깜빡임
                //    if (myTile.TryGetComponent(out Tile myTileScript))
                //        StartCoroutine(myTileScript.Flash());

                //    if (targetTile.TryGetComponent(out Tile targetTileScript))
                //        StartCoroutine(targetTileScript.Flash());

                //    return;
                //}

                ////타일에 아무도 없으면 배치
                //if (tile.IsPlaceable())
                //{
                //    transform.SetParent(tile.transform); // 타일의 자식으로 설정
                //    transform.position = tile.transform.position;

                //    StartCoroutine(tile.Flash());
                //    return;
                //}
            }

            //배치 실패시 원래 자리로 복귀
            if(startParent != null)
            {
                transform.SetParent(startParent);
                transform.position = startParent.position;
            }
        }

        // 적 감지 및 공격 또는 이동
        public void LookForEnemy()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length == 0) return;

            GameObject closest = null;  
            float closestDistance = Mathf.Infinity; //최대값
            
            //가장 가까운 적 찾기
            foreach (GameObject enemy in enemies)
            {
                float dist = Vector3.Distance(transform.position, enemy.transform.position);
                if (dist < closestDistance)
                {
                    closest = enemy;
                    closestDistance = dist;
                }
            }

            if (closest != null)
            {
                float distance = Vector3.Distance(transform.position, closest.transform.position);

                if (distance <= attackRange)
                {
                    //가까이있으면 어택
                    characterMachine.ChangeState(CharacterState.Attacking);
                }
                else if (distance <= detectRange)
                {
                    //사정거리 내에 사격
                    characterMachine.ChangeState(CharacterState.Shooting);
                    ShootTarget(); // 원거리 공격
                }
                else
                {
                    //적방향으로 이동해서 공격
                    characterMachine.ChangeState(CharacterState.Walking);
                    MoveToTarget(closest.transform.position);
                }
            }
        }

        //적을향해 이동
        private void MoveToTarget(Vector3 targetPosition)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            direction.y = 0; //y축 고정
            transform.position += direction * moveSpeed * Time.deltaTime;

            if (direction != Vector3.zero)
            {
                Quaternion rot = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * moveSpeed);
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
