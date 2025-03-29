using UnityEngine;
using UnityEngine.UI;

namespace MyStartEdge
{
    public class Health : MonoBehaviour,IDamageable
    {
        #region Variables
        private float currentHealth;                              //���� ü��
        private CharacterMachine characterMachine;
        private CharacterData characterData;

        public Image healthFill;        

        public bool IsDead => currentHealth <= 0;       //����ó��
        #endregion

        void Start ()
        {
            characterMachine = GetComponent<CharacterMachine>();
            if(characterData != null)
            {
                currentHealth = characterData.maxHealth; // ���� ü���� maxHealth�� �ʱ�ȭ
                UpdateHealthBar();
            }
            else
            {
                Debug.LogWarning("ĳ���� �����Ͱ� ������ �ʾҽ��ϴ�.");
            }
        }

        public void SetCharacterData(CharacterData data)
        {
            characterData = data;
            currentHealth = data.maxHealth;
            UpdateHealthBar();
        }

        //HealthBar ������Ʈ
        private void UpdateHealthBar()
        {
            if (healthFill != null && characterData != null)
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
    }
}