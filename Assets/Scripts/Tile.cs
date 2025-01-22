using UnityEngine;

namespace MyStartEdge
{
    public class Tile : MonoBehaviour
    {
        public bool isOccupied = false; // 유닛이 배치되었는지 체크
        public Unit placedUnit; // 배치된 유닛

        public void PlaceUnit(Unit unit)
        {
            if (isOccupied) return; // 이미 배치된 타일이면 무시
            placedUnit = unit;
            isOccupied = true;
            unit.transform.position = transform.position; // 유닛 배치
        }

        public void RemoveUnit()
        {
            isOccupied = false;
            placedUnit = null;
        }
    }
}
