using UnityEngine;

namespace MyStartEdge
{
    [CreateAssetMenu(fileName = "newCharacter", menuName = "Scriptable Objects/newCharacterData")]
    public class CharacterData : ScriptableObject
    {
        [Header("Data Settings")]
        public int id;                                  //ĳ���� ���� ���̵�
        public string characterName;            //ĳ�����̸�
        public Sprite characterImage;             //ĳ����Sprite
        public GameObject characterPrefab;  //ĳ���� ������
        public int maxHealth;                     //ĳ���� hp
        public int attack;                          //ĳ���� ����
        public float bulletSpeed;                //�Ѿ� �ӵ�
        public int price;                           //ĳ���� ��� ���� 

        [Header("AI Settings")]
        public float detectRange;    // �� ���� ����
        public float attackRange;     // ���� ���� ����
        public float moveSpeed;     // �̵� �ӵ�
    }
}