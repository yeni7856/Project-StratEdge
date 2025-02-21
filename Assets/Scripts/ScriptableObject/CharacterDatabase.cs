using System.Collections.Generic;
using UnityEngine;

namespace MyStartEdge
{
    [CreateAssetMenu(fileName = "CharacterDatabase", menuName = "Scriptable Objects/CharacterDatabase")]
    public class CharacterDatabase : ScriptableObject
    {
        public List<CharacterData> characters; // 캐릭터 목록
    }
}
