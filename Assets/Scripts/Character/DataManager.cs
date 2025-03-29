using System.Collections.Generic;
using UnityEngine;

namespace MyStartEdge
{
    /// <summary>
    /// 데이터 매니저
    /// </summary>
    public class DataManager : MonoBehaviour
    {
        //데이터 싱글톤 인스턴스
        public static DataManager Instance { get; private set; }

        [Header("캐릭터 데이터 리스트")]
        public List<CharacterData> allCharacterData;

        //빠른 검색을 위한 딕셔너리
        private Dictionary<int, CharacterData> characterData;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        //캐릭터 데이터초기화 
        public void Init(CharacterDatabase database)
        {
            characterData = new Dictionary<int, CharacterData>();
            allCharacterData = new List<CharacterData>();

            foreach(var data in database.characters)
            {
                if(data != null && !characterData.ContainsKey(data.id))
                {
                    characterData.Add(data.id, data);
                    allCharacterData.Add(data);
                }
                else
                {
                    Debug.LogWarning($"중복되거나 비어있는 캐릭터 ID : {data?.id}");
                }
            }
            Debug.Log($"데이터메니저 초기화 완료 총 : {allCharacterData.Count} 캐릭터 등록됨");   
        }

        //Id로 캐릭터 데이터 가져오는 메서드
        public CharacterData GetCharacterData(int id)
        {
            if (characterData.TryGetValue(id, out var data))
                return data;
            else
            {
                Debug.LogError("ID" + id + "에 해당하는 캐릭터 데이터가 없음");
                return null;
            }
        }
    }

}
