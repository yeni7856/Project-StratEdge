using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MyStartEdge
{
    public class Tile : MonoBehaviour
    {
        #region Variables
        [Header("Ÿ�� ���̶���Ʈ")]
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

        //�巡��/���� �� Ÿ�� ���̶���Ʈ
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

        //ĳ���� ��ġ ����/ ���� Ÿ�� ����
        public IEnumerator Flash()
        {
            if (tileRenderer == null || flashMaterial == null || originalMaterial == null) yield break;

            tileRenderer.material = flashMaterial;
            yield return new WaitForSeconds(0.3f);
            tileRenderer.material = originalMaterial;
        }

        // ĳ���� ��ġ ���� ���� Ȯ�� //ĳ���Ͱ� ���� ��츸 true ��ȯ
        public bool IsPlaceable()
        {
            CharacterAIController existingCharacter = GetComponentInChildren<CharacterAIController>();
            // �ڽ��� �ڽ� ������Ʈ�� �ִ��� Ȯ��
            return existingCharacter == null;
        }
    }
}