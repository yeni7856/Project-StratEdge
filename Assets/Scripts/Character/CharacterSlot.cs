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
        public void Setup(CharacterData data, System.Action<CharacterData> onSelect)
        {
            characterData = data;
            characterImage.sprite = data.characterImage; // ĳ���� �̹��� ����
            characterName.text = data.characterName; // ĳ���� �̸� ����

            selectButton.onClick.RemoveAllListeners(); // ���� �̺�Ʈ ����
            selectButton.onClick.AddListener(() => onSelect(characterData)); // ���ο� �̺�Ʈ ���
        }
    }
}
