using UnityEngine;

namespace MyStartEdge
{
    public class Tile : MonoBehaviour
    {
        #region Variables
        public bool isOccupied = false; // 타일 위에 몬스터가 있는지 확인
        private Material defaultMaterial;
        [SerializeField] private Material hoverMaterial; // Hover 시 변경할 머티리얼
        #endregion

        void Start()
        {
            defaultMaterial = GetComponent<Renderer>().material;
        }

        private void OnMouseEnter()
        {
            if (isOccupied == true)
            {
                GetComponent<Renderer>().material = hoverMaterial;
            }
        }

        private void OnMouseExit()
        {
            GetComponent<Renderer>().material = defaultMaterial;
        }

    }
}