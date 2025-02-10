using UnityEngine;

namespace MyStartEdge
{
    public class Tile : MonoBehaviour
    {
        #region Variables
        public bool isOccupied = false; // Ÿ�� ���� ���Ͱ� �ִ��� Ȯ��
        private Material defaultMaterial;
        [SerializeField] private Material hoverMaterial; // Hover �� ������ ��Ƽ����
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