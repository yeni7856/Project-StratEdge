using UnityEngine;

namespace MyStartEdge
{

    public class GameManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private CharacterDatabase characterDatabase;
        #endregion
        private void Start()
        {
            DataManager.Instance.Init(characterDatabase);
        }
    }
}
