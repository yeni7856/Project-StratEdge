using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MyStartEdge
{
    public class Tile : MonoBehaviour
    {
        #region Variables
        [Header("타일 하이라이트")]
        [SerializeField] private Material hoverMaterial;
        [SerializeField] private Material flashMaterial;

        private Renderer tileRenderer;
        private Material originalMaterial;
        #endregion

        private void Awake()
        {
            tileRenderer = GetComponent<Renderer>();

            if (tileRenderer != null)
            {
                originalMaterial = GetComponent<Material>();
            }
        }

        private void OnMouseEnter()
        {
            if (IsPlaceable())
            {
                Highlight(true);
            }
        }

        private void OnMouseExit()
        {
            Highlight(false);
        }

        //드래그/오버 시 타일 하이라이트
        public void Highlight(bool on, bool isSwapTarget = false)
        {
            if (tileRenderer == null || hoverMaterial == null || originalMaterial == null)
                return;
            if (!on)
            {
                tileRenderer.material = originalMaterial;
                return;
            }
            tileRenderer.material = isSwapTarget ? flashMaterial : hoverMaterial;
        }

        //캐릭터 배치 성공/ 스왑 타일 변경
        public IEnumerator Flash()
        {
            if (tileRenderer == null || flashMaterial == null || originalMaterial == null) yield break;

            tileRenderer.material = flashMaterial;
            yield return new WaitForSeconds(0.3f);
            tileRenderer.material = originalMaterial;
        }

        // 캐릭터 배치 가능 여부 확인 //캐릭터가 없는 경우만 true 반환
        public bool IsPlaceable()
        {
            CharacterAIController existingCharacter = GetComponentInChildren<CharacterAIController>();
            // 자신의 자식 오브젝트가 있는지 확인
            return existingCharacter == null;
        }
    }
}