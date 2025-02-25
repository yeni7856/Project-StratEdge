using System.Collections.Generic;
using UnityEngine;

namespace MyStartEdge
{
    [CreateAssetMenu(fileName = "CharacterDatabase", menuName = "Scriptable Objects/CharacterDatabase")]
    public class CharacterDatabase : ScriptableObject
    {
        public CharacterData[] characters; // 캐릭터 목록
        private void OnValidate()
        {
            for (int i = 0; i < characters.Length; i++)
            {
                if (characters[i] == null)
                    continue;

                characters[i].id = i;
            }
        }
    }
}
