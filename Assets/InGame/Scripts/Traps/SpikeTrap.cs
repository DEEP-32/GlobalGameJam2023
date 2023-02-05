using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Colliding");
        if (other.collider.tag == "Player")
        {
            var health = other.transform.GetComponent<PlayerHealth>();
            health.Kill();
        }
    }
}
