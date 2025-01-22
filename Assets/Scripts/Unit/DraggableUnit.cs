using UnityEngine;
using UnityEngine.EventSystems;


namespace MyStartEdge
{
    public class DraggableUnit : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private CanvasGroup canvasGroup;
        private Transform parentAfterDrag;

        public GameObject unit3DPrefab; // 3D 유닛 프리팹

        void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            parentAfterDrag = transform.parent;
            transform.SetParent(GameObject.Find("Canvas").transform);
            canvasGroup.blocksRaycasts = false; // 드래그 중 UI 무시
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
                // 3D 유닛 생성
                Instantiate(unit3DPrefab, tile.position, Quaternion.identity);
                Destroy(gameObject); // UI 오브젝트 제거
            }
            else
            {
                transform.SetParent(parentAfterDrag); // 원래 자리로 복귀
                transform.localPosition = Vector3.zero;
            }
        }
    }
}