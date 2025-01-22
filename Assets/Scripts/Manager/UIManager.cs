using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MyStartEdge;

namespace MyStartEdge
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        public TextMeshProUGUI goldText;
        public Button levelUpButton;
        public Button refreshButton;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject); // 인스턴스가 이미 있다면 새로운 인스턴스는 제거
            }
        }

        void Start()
        {
            UpdateGoldUI();
            levelUpButton.onClick.AddListener(LevelUp);
            refreshButton.onClick.AddListener(RefreshShop);
        }

        public void UpdateGoldUI()
        {
            if (goldText != null)
            {
                goldText.text = $"골드: {EconomyManager.Instance.gold}";
            }
            else
            {
                Debug.LogError("goldText is null! UI 텍스트가 할당되지 않았습니다.");
            }
        }

        void LevelUp()
        {
            if (EconomyManager.Instance.SpendGold(4)) // 예제: 4골드 필요
            {
                Debug.Log("레벨업!");
            }
        }

        void RefreshShop()
        {
            if (EconomyManager.Instance.SpendGold(2)) // 예제: 2골드 필요
            {
                Debug.Log("상점 새로고침!");
            }
        }
    }
}