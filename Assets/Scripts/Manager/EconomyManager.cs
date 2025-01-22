using UnityEngine;


namespace MyStartEdge
{
    public class EconomyManager : MonoBehaviour
    {
        public static EconomyManager Instance { get; set; }

        public int gold = 10; // �ʱ� ���
        public int incomePerRound = 5; // ����� �⺻ ����
        public int interestRate = 10; // ������ (10% �߰� ���)
        public int maxInterest = 5; // �ִ� �߰� ��� ����

        public void AddGold(int amount)
        {
            gold += amount;
            Debug.Log("��� �߰�: " + amount + " | ���� ���: " + gold);
        }

        public bool SpendGold(int amount)
        {
            if (gold >= amount)
            {
                gold -= amount;
                Debug.Log("��� ���: " + amount + " ���� ���: " + gold);
                return true;
            }
            else
            {
                Debug.Log("��� ����!");
                return false;
            }
        }
        public void ApplyRoundIncome()
        {
            int interest = Mathf.Min(gold * interestRate / 100, maxInterest);
            AddGold(incomePerRound + interest);
        }
    }
}