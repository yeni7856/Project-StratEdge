using UnityEngine;


namespace MyStartEdge
{
    public class PlayerSpawnTile : MonoBehaviour
    {

        public bool IsPlaceable()
        {
            return transform.childCount == 0;
        }
    }
}