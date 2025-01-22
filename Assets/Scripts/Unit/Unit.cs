using UnityEngine;
using System.Collections;


namespace MyStartEdge
{
    public class Unit : MonoBehaviour
    {
        public int health = 100;
        public int attackDamage = 10;
        public float attackRange = 2f;
        public float attackSpeed = 1.5f;
        private Unit target;

        void Start()
        {
            StartCoroutine(AutoAttack());
        }

        IEnumerator AutoAttack()
        {
            while (health > 0)
            {
                yield return new WaitForSeconds(attackSpeed);
                FindTarget();
                if (target != null)
                {
                    target.TakeDamage(attackDamage);
                }
            }
        }

        void FindTarget()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
            float minDistance = Mathf.Infinity;
            Unit closestEnemy = null;

            foreach (Collider collider in hitColliders)
            {
                Unit enemy = collider.GetComponent<Unit>();
                if (enemy != null && enemy != this) // 자기 자신 제외
                {
                    float distance = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestEnemy = enemy;
                    }
                }
            }
            target = closestEnemy;
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }

        void Die()
        {
            Destroy(gameObject);
        }
    }
}