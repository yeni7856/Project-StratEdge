using UnityEngine;
using UnityEngine.UI;

namespace MyStartEdge
{
    public class Health : MonoBehaviour,IDamageable
    {
        #region Variables
        private float currentHealth;                              //���� ü��
        public bool IsDead => currentHealth <= 0;       //����ó��
        private CharacterMachine characterMachine;
        private CharacterData characterData;
        public Image healthFill;
        #endregion

        void Start ()
        {
            characterMachine = GetComponent<CharacterMachine>();
            currentHealth = characterData.maxHealth; // ���� ü���� maxHealth�� �ʱ�ȭ
            UpdateHealthBar();
        }

        //HealthBar ������Ʈ
        private void UpdateHealthBar()
        {
            if (healthFill != null)
            {
                healthFill.fillAmount = currentHealth / characterData.maxHealth;
            }
        }
        public void Die()
        {
            characterMachine.ChangeState(CharacterState.Dead);
            Destroy(gameObject, 2f);
        }

        public void TakeDamage(float damage)
        {
            if (IsDead || damage <=0 ) return;     //�׾��ų� �������� 0���ϸ� ����

            currentHealth -= damage;
            UpdateHealthBar();

            if (currentHealth <= 0)
            {
                Die();
            }
        }
/*        // �ִ� ü���� �������� �Լ�
        private float GetMaxHealth()
        {
            CharacterAIController controller = GetComponent<CharacterAIController>();
            if (controller != null && controller.characterData != null)
            {
                return controller.characterData.maxHealth;
            }
            return 100f; // �⺻��, �ʿ信 ���� ����
        }*/
    }
}