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
        private Animator animator;
        public Image healthFill;
        #endregion

        void Start ()
        {
            currentHealth = maxHealth;      //초기화
            animator = GetComponent<Animator>();
            characterMachine = GetComponent<CharacterMachine>();
            UpdateHealthBar();
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
            characterMachine.ChangeState(CharacterState.Dead);
            Destroy(gameObject, 2f);
        }

        public void TakeDamage(float damage)
        {
            if (IsDead) return;

            currentHealth -= damage;
            UpdateHealthBar();

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }
}