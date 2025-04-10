using System.Collections.Generic;
using UnityEngine;

namespace MyStartEdge
{
    public class CharacterSlotManager : MonoBehaviour
    {
        #region Variables
        [Header("UI ���� ����")]
        //public CharacterDatabase characterDB;            //ĳ���� �����ͺ��̽�
        public Transform uiSlot;                                //UI �θ� ����
        public GameObject characterSlotPrefab;          // UI ���� ������
        private int slotCount = 5;         //UI ���� ����

        private List<CharacterData> selectedCharacters = new List<CharacterData>();
        #endregion

        private void Start()
        {
            RandomCharacterSlots();
        }


        private void RandomCharacterSlots()
        {
            ClearUI(); // ���� UI ����
            SelectRandomCharacters(); // ���� ĳ���� ����

            // UI ���� ����
            foreach (var character in selectedCharacters)
            {
                CreateCharacterSlot(character);
            }
        }

        //���� UI ���� �Լ�
        private void ClearUI()
        {
            foreach (Transform child in uiSlot)
            {
                Destroy(child.gameObject);
            }
            selectedCharacters.Clear();
        }

        //���� ĳ���� ���� �Լ�
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
        
        //UI ���� ���� �Լ�
        //���� Ŭ���� �ش� ���� ���� �� ĳ���� ����
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
            Debug.Log(character.characterName = "���õ�");
            CharacterSpawner.Instance.SpawnCharacter(character);
        }
    }
}
