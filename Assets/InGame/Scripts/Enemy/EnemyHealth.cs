using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField,Tooltip("Starting health")] private float startHealth;
    [SerializeField, Range(200, 600)] private float maxHealth;

    private Health health;

    private void Awake()
    {
        health = new Health(startHealth, maxHealth);
    }

    public void TakeDamage(float dmgAmount)
    {
        health.CurrentHealth -= dmgAmount;
        if (health.CurrentHealth == 0)
        {
            //Debug.Log("Giving damage");
            Invoke(nameof(Die), 0.5f);
        }
    }

    public void HealUnit(float healAmount)
    {
        health.CurrentHealth += healAmount;
    }

    private void Die()
    {
        if (transform.parent != null)
            Destroy(transform.parent.gameObject);
        Destroy(gameObject);
    }

}
