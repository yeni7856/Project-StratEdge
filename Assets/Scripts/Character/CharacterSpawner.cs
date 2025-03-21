using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyStartEdge
{
    public class CharacterSpawner : MonoBehaviour
    {
        public static CharacterSpawner Instance;
        public static Transform[] spawnPoints; // 캐릭터가 배치될 위치

        /// <summary>
        /// 객체 풀 CharcaterData id 에 해당 캐릭터 프리팹 리스트
        /// </summary>
        private Dictionary<int, List<GameObject>> characterPools = new Dictionary<int, List<GameObject>>();
        private int poolSize = 5;

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

                FindSpawnPoints();
        }

        void FindSpawnPoints()
        {
            // "SpawnPoints" 부모 포지션
            spawnPoints = new Transform[this.transform.childCount];
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                if(spawnPoints != null)
                {
                    spawnPoints[i] = this.transform.GetChild(i);
                    Debug.Log($"🔍 Found {spawnPoints.Length} spawn points.");
                }
                else
                {
                    Debug.LogWarning(" 'SpawnPoints' 오브젝트를 씬에 추가하세요!");
                }
            }
        }

        //객체 풀에서 사용가능한 캐릭터 반환
        private GameObject GetPooledCharacter(CharacterData character)
        {
            int id = character.id;
            if (!characterPools.ContainsKey(id))
            {
                //풀생성
                characterPools[id] = new List<GameObject>();
                for(int i = 0; i < poolSize; i++)
                {
                    GameObject obj = Instantiate(character.characterPrefab, Vector3.zero, Quaternion.identity);
                    obj.SetActive(false);
                    characterPools[id].Add(obj);
                }
            }

            //사용 가능한 객체 검색
            foreach(GameObject obj in characterPools[id])
            {
                if (!obj.activeInHierarchy)
                {
                    return obj;
                }
            }
            //모두 사용중이면 추가 생성 안함
            return null;
        }

        //캐릭터 맵위치에 생성하기
        public void SpawnCharacter(CharacterData character)
        {
            if (spawnPoints.Length == 0)
            {
                Debug.LogWarning("스폰 포인트가 없습니다!");
                return;
            }
            // 랜덤으로 스폰 위치 선택
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // 스폰포인트에 이미 자식(캐릭터)이 있으면 스폰하지 않음
            if (randomSpawnPoint.childCount > 0)
            {
                Debug.Log("스폰포인트에 캐릭터가 이미 존재합니다. 캐릭터 덱을 비워주세요.");
                return;
            }

            // 객체 풀에서 사용 가능한 캐릭터 가져오기
            GameObject characterInstance = GetPooledCharacter(character);
            if (characterInstance == null)
            {
                Debug.Log("풀에 사용 가능한 캐릭터가 없습니다!");
                return;
            }
            // 캐릭터 인스턴스 생성
            Instantiate(character.characterPrefab, randomSpawnPoint.position, Quaternion.identity);

            // 생성된 캐릭터에 데이터 할당 및 상태 초기화 (Idle 상태)
            CharacterAIController aiController = characterInstance.GetComponent<CharacterAIController>();
            if (aiController != null)
            {
                aiController.characterData = character;
            }
/*            Health health = characterInstance.GetComponent<Health>();
            if (health != null)
            {
                health.characterData = character;
                // Health.Start()에서 maxHealth를 반영하게 됨
            }*/
            CharacterMachine machine = characterInstance.GetComponent<CharacterMachine>();
            if (machine != null)
            {
                machine.ChangeState(CharacterState.Idle);
            }
        }
    }
}