using UnityEngine;

namespace MyStartEdge
{
    public class CharacterSpawner : MonoBehaviour
    {
        public static CharacterSpawner Instance;
        public Transform[] spawnPoints; // ĳ���Ͱ� ��ġ�� ��ġ

        private void Awake()
        {
            Instance = this;
        }

        public void SpawnCharacter(CharacterData character)
        {
            if (spawnPoints.Length == 0)
            {
                Debug.LogWarning("���� ����Ʈ�� �����ϴ�!");
                return;
            }
            // �������� ���� ��ġ ����
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // ĳ���� �ν��Ͻ� ����
            Instantiate(character.characterPrefab, randomSpawnPoint.position, Quaternion.identity);
        }
    }
}