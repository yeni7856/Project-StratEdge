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
        public CharacterDatabase characterDB;            //ĳ���� �����ͺ��̽�
        public Transform uiContainer;                      //UI �θ� ����
        public GameObject characterSlotPrefab;          // UI ���� ������

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
            foreach (Transform child in uiContainer)
            {
                Destroy(child.gameObject);
            }
            selectedCharacters.Clear();
        }

        //���� ĳ���� ���� �Լ�
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
        
        //UI ���� ���� �Լ�
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
            Debug.Log(character.characterName = "���õ�");
            CharacterSpawner.Instance.SpawnCharacter(character);
        }
    }
}
