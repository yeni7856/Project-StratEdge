using UnityEngine;
using UnityEngine.UI;

namespace MyStartEdge
{
    public class Health : MonoBehaviour,IDamageable
    {
        #region Variables
        [SerializeField] private int maxHealth = 100;       //최대 체력
        private float currentHealth;                                  //현재 체력
        public bool IsDead => currentHealth <= 0;       //죽음처리

        private CharacterMachine characterMachine;
        public Image healthFill;
        #endregion

        void Start ()
        {
            currentHealth = maxHealth;      //초기화
            characterMachine = GetComponent<CharacterMachine>();
            UpdateHealthBar();
        }

        //HealthBar 업데이트
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
            if (IsDead || damage <=0 ) return;     //죽었거나 데미지가 0이하면 무시

            currentHealth -= damage;
            UpdateHealthBar();

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }
}