using UnityEngine;
using UnityEngine.UI;

namespace MyStartEdge
{
    public class Health : MonoBehaviour
    {
        #region Variables
        [SerializeField] private int maxHealth = 100;       //�ִ� ü��
        private int currentHealth;                                  //���� ü��
        public bool IsDead => currentHealth <= 0;       //����ó��

        private CharacterStateMachine characterStateMachine;
        private Animator animator;
        public Image healthFill;
        #endregion

        void Start ()
        {
            currentHealth = maxHealth;      //�ʱ�ȭ
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