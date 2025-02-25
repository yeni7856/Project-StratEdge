using System.Linq;
using UnityEngine;

namespace MyStartEdge
{
    public class CharacterSpawner : MonoBehaviour
    {
        public static CharacterSpawner Instance;
        public static Transform[] spawnPoints; // 캐릭터가 배치될 위치

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
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

            // 캐릭터 인스턴스 생성
            Instantiate(character.characterPrefab, randomSpawnPoint.position, Quaternion.identity);
        }
    }
}