using UnityEngine;

namespace MyStartEdge
{
    [CreateAssetMenu(fileName = "newCharacter", menuName = "Scriptable Objects/newCharacterData")]
    public class CharacterData : ScriptableObject
    {
        [Header("Data Settings")]
        public int id;                                  //캐릭터 고유 아이디
        public string characterName;            //캐릭터이름
        public Sprite characterImage;             //캐릭터Sprite
        public GameObject characterPrefab;  //캐릭터 프리팹
        public int maxHealth;                     //캐릭터 hp
        public int attack;                          //캐릭터 어택
        public float bulletSpeed;                //총알 속도
        public int price;                           //캐릭터 골드 가격 

        [Header("AI Settings")]
        public float detectRange;    // 적 감지 범위
        public float attackRange;     // 근접 공격 범위
        public float moveSpeed;     // 이동 속도
    }
}