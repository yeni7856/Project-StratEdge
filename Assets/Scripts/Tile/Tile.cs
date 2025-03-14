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

        // ĳ���� ��ġ ���� ���� Ȯ��
        public bool IsPlaceable()
        {
            // �ڽ��� �ڽ� ������Ʈ�� �ִ��� Ȯ��
            return transform.childCount == 0;
        }

        // Ÿ�� ���� ĳ���Ͱ� �ִ��� Ȯ���ϴ� �Լ�
        public bool HasCharactersOnTile()
        {
            // Ÿ���� �߽ɿ��� �������� Raycast�� ��Ƽ� ĳ���Ͱ� �ִ��� Ȯ��
            Ray ray = new Ray(transform.position + Vector3.up * 0.5f, Vector3.down); // Ÿ�� �ణ ������ �Ʒ���
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1f)) // Raycast �Ÿ� 1f
            {
                // Ray�� ���� ������Ʈ�� CharacterAIController�� ������ �ִ��� Ȯ��
                if (hit.collider.GetComponent<CharacterAIController>() != null)
                {
                    return true; // ĳ���Ͱ� ����
                }
            }
            return false; // ĳ���Ͱ� ����
        }

        // Ÿ�� ���� �ִ� ĳ���͵鿡�� �����
        public void OrderAttack()
        {
            if (HasCharactersOnTile())
            {
                // Ÿ���� �߽ɿ��� �������� Raycast�� ��Ƽ� ĳ���͸� ã��
                Ray ray = new Ray(transform.position + Vector3.up * 0.5f, Vector3.down);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1f))
                {
                    CharacterAIController character = hit.collider.GetComponent<CharacterAIController>();
                    if (character != null)
                    {
                        // ĳ���Ϳ��� ���� ����� ������ ���� (��: character.Attack();)
                        // CharacterAIController�� Attack() �Լ��� �߰��ؾ� �մϴ�.
                    }
                }
            }
        }
    }
}