using UnityEngine;
using UnityEngine.EventSystems;

namespace MyStartEdge
{
    public class TileManager : MonoBehaviour
    {
        public static TileManager Instance;
        private Transform hoveringTile;

        void Awake()
        {
            Instance = this;
        }

        void Update()
        {
            DetectTileHover();
        }

        void DetectTileHover()
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.collider.CompareTag("Tile"))
                {
                    if (hoveringTile != hit.transform)
                    {
                        if (hoveringTile != null)
                            hoveringTile.GetComponent<Renderer>().material.color = Color.white; // 원래 색으로

                        hoveringTile = hit.transform;
                        hoveringTile.GetComponent<Renderer>().material.color = Color.yellow; // 호버 효과
                    }
                }
            }
            else if (hoveringTile != null)
            {
                hoveringTile.GetComponent<Renderer>().material.color = Color.white; // 원래 색으로
                hoveringTile = null;
            }
        }

        public bool IsHoveringTile(out Transform tile)
        {
            tile = hoveringTile;
            return tile != null;
        }
    }
}