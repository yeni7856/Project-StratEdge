using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyStartEdge
{
    public class CharacterSlot : MonoBehaviour
    {
        #region Varables
        public Image characterImage;                    //캐릭터 이미지
        public TextMeshProUGUI characterName;    //캐릭터 이름   
        public TextMeshProUGUI characterGold;      //캐릭터 골드 
        public Button selectButton;                       //버튼  

        private CharacterData characterData;        //현재 슬롯 캐릭터 데이터
        #endregion

        // 캐릭터 정보 업데이트
        public void Setup(CharacterData data, System.Action<CharacterData> onSelect)
        {
            characterData = data;
            characterImage.sprite = data.characterImage; // 캐릭터 이미지 설정
            characterName.text = data.characterName; // 캐릭터 이름 설정

            selectButton.onClick.RemoveAllListeners(); // 기존 이벤트 삭제
            selectButton.onClick.AddListener(() => onSelect(characterData)); // 새로운 이벤트 등록
        }
    }
}
