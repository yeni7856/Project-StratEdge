using UnityEngine;
using System.Collections;


namespace MyStartEdge
{
    public class GameManager : MonoBehaviour
    {
        public float preparationTime = 10f;
        public float battleTime = 20f;
        public int roundNumber = 1;

        void Start()
        {
            StartCoroutine(GameLoop());
        }

        IEnumerator GameLoop()
        {
            while (true)
            {
                Debug.Log("라운드 " + roundNumber + " 시작! 유닛을 배치하세요.");
                yield return new WaitForSeconds(preparationTime);

                Debug.Log("전투 시작!");
                StartBattle();
                yield return new WaitForSeconds(battleTime);

                Debug.Log("전투 종료! 다음 라운드로 진행합니다.");
                roundNumber++;
            }
        }

        void StartBattle()
        {
            Unit[] allUnits = FindObjectsByType<Unit>(FindObjectsSortMode.None);
            foreach (Unit unit in allUnits)
            {
                unit.StartCoroutine("AutoAttack");
            }
        }
        void EndRound()
        {
            EconomyManager.Instance.ApplyRoundIncome();
            Debug.Log("라운드 종료! 골드 업데이트됨.");
        }
    }
}