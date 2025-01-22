using UnityEngine;
using UnityEngine.EventSystems;


namespace MyStartEdge
{
    public class DraggableUnit : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private CanvasGroup canvasGroup;
        private Transform parentAfterDrag;

        public GameObject unit3DPrefab; // 3D ���� ������

        void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            parentAfterDrag = transform.parent;
            transform.SetParent(GameObject.Find("Canvas").transform);
            canvasGroup.blocksRaycasts = false; // �巡�� �� UI ����
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = true;

            if (TileManager.Instance.IsHoveringTile(out Transform tile))
            {
                // 3D ���� ����
                Instantiate(unit3DPrefab, tile.position, Quaternion.identity);
                Destroy(gameObject); // UI ������Ʈ ����
            }
            else
            {
                transform.SetParent(parentAfterDrag); // ���� �ڸ��� ����
                transform.localPosition = Vector3.zero;
            }
        }
    }
}