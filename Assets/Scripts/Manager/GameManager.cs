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
                Debug.Log("���� " + roundNumber + " ����! ������ ��ġ�ϼ���.");
                yield return new WaitForSeconds(preparationTime);

                Debug.Log("���� ����!");
                StartBattle();
                yield return new WaitForSeconds(battleTime);

                Debug.Log("���� ����! ���� ����� �����մϴ�.");
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
            Debug.Log("���� ����! ��� ������Ʈ��.");
        }
    }
}