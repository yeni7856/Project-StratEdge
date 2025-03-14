using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MyStartEdge
{
    public class Tile : MonoBehaviour
    {
        #region Variables
        /*[SerializeField] private Color hoverColor = Color.green;*/
        private Renderer tileRenderer;
        private Material originalMaterial;
        [SerializeField] private Material hoverMaterial;
        #endregion

        private void Awake()
        {
            tileRenderer = GetComponent<Renderer>();
            originalMaterial = tileRenderer.material;
        }

        public void OnMouseEnter()
        {
            tileRenderer.material = hoverMaterial;
        }

        public void OnMouseExit()
        {
            tileRenderer.material = originalMaterial;
        }

        // 캐릭터 배치 가능 여부 확인
        public bool IsPlaceable()
        {
            // 자신의 자식 오브젝트가 있는지 확인
            return transform.childCount == 0;
        }

        // 타일 위에 캐릭터가 있는지 확인하는 함수
        public bool HasCharactersOnTile()
        {
            // 타일의 중심에서 위쪽으로 Raycast를 쏘아서 캐릭터가 있는지 확인
            Ray ray = new Ray(transform.position + Vector3.up * 0.5f, Vector3.down); // 타일 약간 위에서 아래로
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1f)) // Raycast 거리 1f
            {
                // Ray에 맞은 오브젝트가 CharacterAIController를 가지고 있는지 확인
                if (hit.collider.GetComponent<CharacterAIController>() != null)
                {
                    return true; // 캐릭터가 있음
                }
            }
            return false; // 캐릭터가 없음
        }

        // 타일 위에 있는 캐릭터들에게 명령을
        public void OrderAttack()
        {
            if (HasCharactersOnTile())
            {
                // 타일의 중심에서 위쪽으로 Raycast를 쏘아서 캐릭터를 찾음
                Ray ray = new Ray(transform.position + Vector3.up * 0.5f, Vector3.down);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1f))
                {
                    CharacterAIController character = hit.collider.GetComponent<CharacterAIController>();
                    if (character != null)
                    {
                        // 캐릭터에게 공격 명령을 내리는 로직 (예: character.Attack();)
                        // CharacterAIController에 Attack() 함수를 추가해야 합니다.
                    }
                }
            }
        }
    }
}