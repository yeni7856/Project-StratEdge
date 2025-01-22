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
                Destroy(gameObject); // �ν��Ͻ��� �̹� �ִٸ� ���ο� �ν��Ͻ��� ����
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
                goldText.text = $"���: {EconomyManager.Instance.gold}";
            }
            else
            {
                Debug.LogError("goldText is null! UI �ؽ�Ʈ�� �Ҵ���� �ʾҽ��ϴ�.");
            }
        }

        void LevelUp()
        {
            if (EconomyManager.Instance.SpendGold(4)) // ����: 4��� �ʿ�
            {
                Debug.Log("������!");
            }
        }

        void RefreshShop()
        {
            if (EconomyManager.Instance.SpendGold(2)) // ����: 2��� �ʿ�
            {
                Debug.Log("���� ���ΰ�ħ!");
            }
        }
    }
}