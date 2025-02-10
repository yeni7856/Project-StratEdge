using UnityEngine;
using UnityEngine.UI;

namespace MyStartEdge
{
    public class Health : MonoBehaviour
    {
        #region Variables
        [SerializeField] private int maxHealth = 100;       //최대 체력
        private int currentHealth;                                  //현재 체력
        public bool IsDead => currentHealth <= 0;       //죽음처리

        private CharacterStateMachine characterStateMachine;
        private Animator animator;
        public Image healthFill;
        #endregion

        void Start ()
        {
            currentHealth = maxHealth;      //초기화
            animator = GetComponent<Animator>();
            characterStateMachine = GetComponent<CharacterStateMachine>();
            UpdateHealthBar();
        }

        public void TakeDamage(int amount)
        {
            if (IsDead) return;

            currentHealth -= amount;
            UpdateHealthBar();

            if (currentHealth <= 0)
            {
                Die();
            }
        }
        private void UpdateHealthBar()
        {
            if (healthFill != null)
            {
                SetHealth(currentHealth, maxHealth);
            }
        }
        public void SetHealth(float currentHealth, float maxHealth)
        {
            healthFill.fillAmount = currentHealth / maxHealth;
        }
        private void Die()
        {
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsShooting", false);
            animator.SetBool("IsAttacking", false);
            animator.SetBool("AllEnemiesDefeated", false);

            animator.SetInteger("Health",0);
            characterStateMachine.ChangeState(CharacterState.Die);
            Destroy(gameObject, 2f);
        }
    }
}