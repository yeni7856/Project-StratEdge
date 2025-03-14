using UnityEngine;
using UnityEngine.UI;

namespace MyStartEdge
{
    public class Health : MonoBehaviour,IDamageable
    {
        #region Variables
        private float currentHealth;                              //현재 체력
        public bool IsDead => currentHealth <= 0;       //죽음처리
        private CharacterMachine characterMachine;
        private CharacterData characterData;
        public Image healthFill;
        #endregion

        void Start ()
        {
            characterMachine = GetComponent<CharacterMachine>();
            currentHealth = characterData.maxHealth; // 현재 체력을 maxHealth로 초기화
            UpdateHealthBar();
        }

        //HealthBar 업데이트
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
            if (IsDead || damage <=0 ) return;     //죽었거나 데미지가 0이하면 무시

            currentHealth -= damage;
            UpdateHealthBar();

            if (currentHealth <= 0)
            {
                Die();
            }
        }
/*        // 최대 체력을 가져오는 함수
        private float GetMaxHealth()
        {
            CharacterAIController controller = GetComponent<CharacterAIController>();
            if (controller != null && controller.characterData != null)
            {
                return controller.characterData.maxHealth;
            }
            return 100f; // 기본값, 필요에 따라 변경
        }*/
    }
}