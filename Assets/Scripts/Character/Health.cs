using UnityEngine;
using UnityEngine.UI;

namespace MyStartEdge
{
    public class Health : MonoBehaviour,IDamageable
    {
        #region Variables
        [SerializeField] private int maxHealth = 100;       //�ִ� ü��
        private float currentHealth;                                  //���� ü��
        public bool IsDead => currentHealth <= 0;       //����ó��

        private CharacterMachine characterMachine;
        public Image healthFill;
        #endregion

        void Start ()
        {
            currentHealth = maxHealth;      //�ʱ�ȭ
            characterMachine = GetComponent<CharacterMachine>();
            UpdateHealthBar();
        }

        //HealthBar ������Ʈ
        private void UpdateHealthBar()
        {
            if (healthFill != null)
            {
                healthFill.fillAmount = currentHealth / maxHealth;
            }
        }
        private void Die()
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