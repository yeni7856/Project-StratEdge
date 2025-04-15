using UnityEngine;


namespace MyStartEdge
{
    public class PlayerSpawnTile : MonoBehaviour
    {
/*        [Header("타일 하이라이트")]
        [SerializeField] private Material hoverMaterial;
        private Renderer tileRenderer;
        private Material originalMaterial;

        private void Awake()
        {
            tileRenderer = GetComponent<Renderer>();

            if(tileRenderer != null)
            {
                originalMaterial = GetComponent<Material>();
            }
        }
        private void OnMouseEnter()
        {
            if(IsPlaceable() && tileRenderer != null)
            {
                tileRenderer.material = hoverMaterial;
            }
        }

        private void OnMouseExit()
        {
            if(tileRenderer != null && originalMaterial != null)
            {
                tileRenderer.material = originalMaterial;
            }
        }*/

        public bool IsPlaceable()
        {
            CharacterAIController existingCharacter = GetComponentInChildren<CharacterAIController>();
            return existingCharacter == null;
        }
    }
}