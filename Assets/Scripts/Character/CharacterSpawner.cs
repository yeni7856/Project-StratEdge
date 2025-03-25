using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyStartEdge
{
    /// <summary>
    /// 캐릭터 덱 
    /// </summary>
    public class CharacterSpawner : MonoBehaviour
    {
        //스포너 인스턴스
        public static CharacterSpawner Instance;
        //public static Transform[] spawnPoints; // 캐릭터가 배치될 위치

        //캐릭터 덱(스폰) 포인트
        [Header("스폰포인트 리스트")]
        [SerializeField]private List<Transform> spawnPoints = new List<Transform>();

        private string spawnPoint = "SpawnPoint";

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            //자동 스폰포인트 등록
            if(spawnPoints.Count == 0)
            {
                FindSpawnPoints();
            }
        }

        void FindSpawnPoints()
        {
            spawnPoints.Clear();
            foreach(Transform child in transform)
            {
                spawnPoints.Add(child);
                child.tag = spawnPoint;
            }

            Debug.Log($"{spawnPoints.Count} spawn points");
        }

        //사용 가능한 스폰포인트 랜덤 선택
        private Transform GetAvailableSpawnPoint()
        {
            List<Transform> availablePoints = new List<Transform>();

            foreach(var sp in spawnPoints)
            {
                if (sp.childCount == 0)
                    availablePoints.Add(sp);
            }
            if(availablePoints.Count == 0)
                return null;
            return availablePoints[Random.Range(0, availablePoints.Count)];
        }

        //캐릭터 맵위치에 생성하기
        public void SpawnCharacter(CharacterData character)
        {
            if (spawnPoints.Count == 0)
            {
                Debug.LogWarning("스폰 포인트가 없습니다!");
                return;
            }
            // 랜덤으로 스폰 위치 선택
            Transform spawnPoint = GetAvailableSpawnPoint();

            // 스폰포인트에 이미 자식(캐릭터)이 있으면 스폰하지 않음
            if (spawnPoint == null)
            {
                Debug.Log("스폰포인트에 캐릭터가 이미 존재합니다. 캐릭터 덱을 비워주세요.");
                return;
            }

            //캐릭터 인스턴스 생성
            GameObject characterInstance = Instantiate(character.characterPrefab, spawnPoint.position, Quaternion.identity);
            characterInstance.transform.SetParent(spawnPoint);
            if (characterInstance == null)
            {
                Debug.Log("캐릭터가 없습니다!");
                return;
            }

            //데이터 메니저 데이터 가져오기
            CharacterData data = DataManager.Instance.GetCharacterData(character.id);

            // 생성된 캐릭터에 데이터 할당 및 상태 초기화 (Idle 상태)
            CharacterAIController aiController = characterInstance.GetComponent<CharacterAIController>();
            if (aiController != null)
            {
                aiController.characterData = data;
            }
            //Health health = characterInstance.GetComponent<Health>();
            //if (health != null)
            //{
            //    health. = character;
            //    // Health.Start()에서 maxHealth를 반영하게 됨
            //}
            CharacterMachine machine = characterInstance.GetComponent<CharacterMachine>();
            if (machine != null)
            {
                machine.ChangeState(CharacterState.Idle);
            }
        }
    }
}