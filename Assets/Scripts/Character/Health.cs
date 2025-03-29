using UnityEngine;
using UnityEngine.UI;

namespace MyStartEdge
{
    public class Health : MonoBehaviour,IDamageable
    {
        #region Variables
        private float currentHealth;                              //현재 체력
        private CharacterMachine characterMachine;
        private CharacterData characterData;

        public Image healthFill;        

        public bool IsDead => currentHealth <= 0;       //죽음처리
        #endregion

        void Start ()
        {
            characterMachine = GetComponent<CharacterMachine>();
            if(characterData != null)
            {
                currentHealth = characterData.maxHealth; // 현재 체력을 maxHealth로 초기화
                UpdateHealthBar();
            }
            else
            {
                Debug.LogWarning("캐릭터 데이터가 들어오지 않았습니다.");
            }
        }

        public void SetCharacterData(CharacterData data)
        {
            characterData = data;
            currentHealth = data.maxHealth;
            UpdateHealthBar();
        }

        //HealthBar 업데이트
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