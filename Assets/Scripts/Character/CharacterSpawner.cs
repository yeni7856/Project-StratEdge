using UnityEngine;

namespace MyStartEdge
{
    public class CharacterSpawner : MonoBehaviour
    {
        public static CharacterSpawner Instance;
        public Transform[] spawnPoints; // 캐릭터가 배치될 위치

        private void Awake()
        {
            Instance = this;
        }

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