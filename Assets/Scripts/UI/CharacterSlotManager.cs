using System.Collections.Generic;
using UnityEngine;

namespace MyStartEdge
{
    public class CharacterSlotManager : MonoBehaviour
    {
        #region Variables
        [Header("UI 슬롯 설정")]
        //public CharacterDatabase characterDB;            //캐릭터 데이터베이스
        public Transform uiSlot;                                //UI 부모 슬롯
        public GameObject characterSlotPrefab;          // UI 슬롯 프리팹
        private int slotCount = 5;         //UI 슬롯 갯수

        private List<CharacterData> selectedCharacters = new List<CharacterData>();
        #endregion

        private void Start()
        {
            RandomCharacterSlots();
        }


        private void RandomCharacterSlots()
        {
            ClearUI(); // 기존 UI 정리
            SelectRandomCharacters(); // 랜덤 캐릭터 선택

            // UI 슬롯 생성
            foreach (var character in selectedCharacters)
            {
                CreateCharacterSlot(character);
            }
        }

        //기존 UI 정리 함수
        private void ClearUI()
        {
            foreach (Transform child in uiSlot)
            {
                Destroy(child.gameObject);
            }
            selectedCharacters.Clear();
        }

        //랜덤 캐릭터 선택 함수
        private void SelectRandomCharacters()
        {
            List<CharacterData> pool = DataManager.Instance.allCharacterData;

            for (int i = 0; i < slotCount; i++)
            {
                if(pool.Count == 0) break;
                int randomIndex = Random.Range(0, pool.Count);
                CharacterData randomCharacter = pool[randomIndex];
                if (randomCharacter != null)
                {
                    selectedCharacters.Add(randomCharacter);
                }

            }
        }
        
        //UI 슬롯 생성 함수
        //슬롯 클릭시 해당 슬롯 제거 후 캐릭터 스폰
        private void CreateCharacterSlot(CharacterData character)
        {
            GameObject slotObj = Instantiate(characterSlotPrefab, uiSlot);
            CharacterSlot slot = slotObj.GetComponent<CharacterSlot>();

            if (slot != null)
            {
                slot.Setup(character, () => {

                    Destroy(slotObj);
                    CharacterOnMap(character);
                });
            }
        }

        private void CharacterOnMap(CharacterData character)
        {
            Debug.Log(character.characterName = "선택됨");
            CharacterSpawner.Instance.SpawnCharacter(character);
        }
    }
}
