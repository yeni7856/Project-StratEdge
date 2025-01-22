using UnityEngine;


namespace MyStartEdge
{
    public class EconomyManager : MonoBehaviour
    {
        public static EconomyManager Instance { get; set; }

        public int gold = 10; // 초기 골드
        public int incomePerRound = 5; // 라운드당 기본 수입
        public int interestRate = 10; // 이자율 (10% 추가 골드)
        public int maxInterest = 5; // 최대 추가 골드 제한

        public void AddGold(int amount)
        {
            gold += amount;
            Debug.Log("골드 추가: " + amount + " | 현재 골드: " + gold);
        }

        public bool SpendGold(int amount)
        {
            if (gold >= amount)
            {
                gold -= amount;
                Debug.Log("골드 사용: " + amount + " 남은 골드: " + gold);
                return true;
            }
            else
            {
                Debug.Log("골드 부족!");
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