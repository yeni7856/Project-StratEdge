using System.Collections.Generic;
using UnityEngine;

namespace MyStartEdge
{
    /// <summary>
    /// ������ �Ŵ���
    /// </summary>
    public class DataManager : MonoBehaviour
    {
        //������ �̱��� �ν��Ͻ�
        public static DataManager Instance { get; private set; }

        [Header("ĳ���� ������ ����Ʈ")]
        public List<CharacterData> allCharacterData;

        //���� �˻��� ���� ��ųʸ�
        private Dictionary<int, CharacterData> characterData;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeData();
            }
            else
            {
                Destroy(gameObject);
            }
        }
        void InitializeData()
        {
            characterData = new Dictionary<int, CharacterData>();

            foreach(var data in allCharacterData)
            {
                if (!characterData.ContainsKey(data.id))
                {
                    characterData.Add(data.id, data);
                }
                else
                {
                    Debug.LogWarning("�ߺ��� ĳ���� ID �߰�" + data.id);
                }
            }
        }
        //ĳ���� ������ �������� �޼���
        public CharacterData GetCharacterData(int id)
        {
            if (characterData.TryGetValue(id, out var data))
                return data;
            else
            {
                Debug.LogError("ID" + id + "�� �ش��ϴ� ĳ���� �����Ͱ� ����");
                return null;
            }
        }
    }

}
