using UnityEngine;

namespace MyStartEdge
{
    public class Tile : MonoBehaviour
    {
        public bool isOccupied = false; // ������ ��ġ�Ǿ����� üũ
        public Unit placedUnit; // ��ġ�� ����

        public void PlaceUnit(Unit unit)
        {
            if (isOccupied) return; // �̹� ��ġ�� Ÿ���̸� ����
            placedUnit = unit;
            isOccupied = true;
            unit.transform.position = transform.position; // ���� ��ġ
        }

        public void RemoveUnit()
        {
            isOccupied = false;
            placedUnit = null;
        }
    }
}
