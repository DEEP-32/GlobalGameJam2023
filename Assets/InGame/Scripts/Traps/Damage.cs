using UnityEngine;

public class Damage : MonoBehaviour
{

    [SerializeField] private float _damageAmount = 2f;
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Hi");
        Debug.Log(other.tag);
        if (other.tag == "Player")
        {
            Debug.Log("Damaging");
            var health = other.GetComponent<PlayerHealth>();
            health.TakeDamage(_damageAmount);
            Debug.Log(health.CurrentHealth);
        }
    }
}