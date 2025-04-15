using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyStartEdge
{
    public class CharacterSlot : MonoBehaviour
    {
        #region Varables
        public Image characterImage;                    //ĳ���� �̹���
        public TextMeshProUGUI characterName;    //ĳ���� �̸�   
        public TextMeshProUGUI characterGold;      //ĳ���� ��� 
        public Button selectButton;                       //��ư  

        private CharacterData characterData;        //���� ���� ĳ���� ������
        #endregion

        // ĳ���� ���� ������Ʈ
        public void Setup(CharacterData character, Action onSelect)
        {
            characterData = character;
            characterImage.sprite = character.characterImage; // ĳ���� �̹��� ����
            characterName.text = character.characterName; // ĳ���� �̸� ����
            characterGold.text = character.price.ToString();   //ĳ���� ��� ���� ����

            selectButton.onClick.RemoveAllListeners(); // ���� �̺�Ʈ ����
            selectButton.onClick.AddListener(() => onSelect.Invoke()); // ���ο� �̺�Ʈ ���
        }
    }
}
