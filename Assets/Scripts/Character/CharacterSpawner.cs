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
        [Header("플레이어 덱 타일 리스트")]
        [SerializeField] private Transform tileParent;
        [SerializeField]private List<PlayerSpawnTile> playerTiles = new List<PlayerSpawnTile>();

        //private string spawnPoint = "SpawnPoint";

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

            //플레이어 덱 타일 자식 컴포넌트 자동수집
            PlayerSpawnTile[] foundTiles = tileParent.GetComponentsInChildren<PlayerSpawnTile>();
            playerTiles = new List<PlayerSpawnTile>(foundTiles);
        }

        //캐릭터 맵위치에 생성하기
        public void SpawnCharacter(CharacterData character)
        {
            //사용가능한 타일
            List<PlayerSpawnTile> availableTiles = new List<PlayerSpawnTile>();
            foreach(PlayerSpawnTile tile in playerTiles)
            {
                if (tile.IsPlaceable())
                {
                    availableTiles.Add(tile);
                }
            }

            //랜덤 사용가능한 타일 없을때
            if (availableTiles.Count == 0)
            {
                Debug.LogWarning("캐릭터 덱을 비워주세요!");
                return;
            }

            // 랜덤으로 타일 위치 선택
            int randomIndex = Random.Range(0, availableTiles.Count);
            PlayerSpawnTile selectedTile = availableTiles[randomIndex];

            // 플레이어 타일에 이미 자식(캐릭터)이 있으면 스폰하지 않음
            if (!selectedTile.IsPlaceable())
            {
                Debug.Log("스폰포인트에 캐릭터가 이미 존재합니다. 캐릭터 덱을 비워주세요.");
                return;
            }

            //캐릭터 생성
            GameObject newcharacter = Instantiate(character.characterPrefab, selectedTile.transform.position, Quaternion.identity, selectedTile.transform);
            if (newcharacter == null)
            {
                Debug.Log("캐릭터가 없습니다!");
                return;
            }

            // 생성된 캐릭터에 데이터 할당 및 상태 초기화 (Idle 상태)
            CharacterAIController aiController = newcharacter.GetComponent<CharacterAIController>();
            if (aiController != null)
            {
                aiController.SetCharacterData(character);
            }
            Health health = newcharacter.GetComponent<Health>();
            if (health != null)
            {
                health.SetCharacterData(character);
            }
            CharacterMachine machine = newcharacter.GetComponent<CharacterMachine>();
            if (machine != null)
            {
                machine.ChangeState(CharacterState.Idle);
            }
            Debug.Log($"캐릭터 {character.characterName} 생성 완료");
        }
    }
}