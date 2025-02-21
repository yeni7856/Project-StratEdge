using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

namespace MyStartEdge
{
    public class CharacterSlotManager : MonoBehaviour
    {
        #region Variables
        public CharacterDatabase characterDB;            //캐릭터 데이터베이스
        public Transform uiContainer;                      //UI 부모 슬롯
        public GameObject characterSlotPrefab;          // UI 슬롯 프리팹

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
            foreach (Transform child in uiContainer)
            {
                Destroy(child.gameObject);
            }
            selectedCharacters.Clear();
        }

        //랜덤 캐릭터 선택 함수
        private void SelectRandomCharacters()
        {
            List<CharacterData> pool = new List<CharacterData>(characterDB.characters);

            for (int i = 0; i < 4 && pool.Count > 0; i++)
            {
                int randomIndex = Random.Range(0, pool.Count);
                selectedCharacters.Add(pool[randomIndex]);
                pool.RemoveAt(randomIndex);
            }
        }
        
        //UI 슬롯 생성 함수
        private void CreateCharacterSlot(CharacterData character)
        {
            GameObject slotObj = Instantiate(characterSlotPrefab, uiContainer);
            CharacterSlot slot = slotObj.GetComponent<CharacterSlot>();

            if (slot != null)
            {
                slot.Setup(character, CharacterOnMap);
            }
        }

        private void CharacterOnMap(CharacterData character)
        {
            Debug.Log(character.characterName = "선택됨");
            CharacterSpawner.Instance.SpawnCharacter(character);
        }
    }
}
