using UnityEngine;

public class Damage : MonoBehaviour
{

    [SerializeField] private float _damageAmount = 2f;
    private void OnParticleCollision(GameObject other)
    {
        if(other.tag == "PLayer")
        {
            var health  = other.GetComponent<PlayerHealth>();
            health.TakeDamage(_damageAmount);
        }
    }
}
