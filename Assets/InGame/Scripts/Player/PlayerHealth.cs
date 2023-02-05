using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField, Range(100, 200),Tooltip("Starting health")] private float startHealth;

   

    [Header("On Damage Parameters")]
   // [SerializeField] private float changedColorTime = .2f;
    public static Action<float> OnTakeDamage;

    public static Action OnDie;

    private Health health;
    public float CurrentHealth => health.CurrentHealth;

    public GameObject RestartB;
    private void Awake()
    {
        health = new Health(startHealth);
        //Debug.Log($"Starting health is: {startHealth}");
        Time.timeScale = 1;
    }
    private void Update()
    {
            
        if(health.CurrentHealth <= 0)
        {
            Time.timeScale = 0;
            RestartB.SetActive(true);
        }
    }
    public void TakeDamage(float dmgAmount)
    {
        health.CurrentHealth -= dmgAmount;
        Debug.Log($"Player Current health is: {health.CurrentHealth}");
        if (health.CurrentHealth <= 0)
        {
            Die();
            return;
        }
        OnTakeDamage?.Invoke(health.CurrentHealth);
        //For future references.
        //StartCoroutine(damageFlash());
    }

    public void HealUnit(float healAmount)
    {
        health.CurrentHealth += healAmount;
    }

    public void Kill()
    {
        TakeDamage(CurrentHealth);
    }

    private void Die()
    {
        Debug.Log("Player Died");
        //Destroy(this.gameObject);
        OnDie?.Invoke();
       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void RestartSceme(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    /*private IEnumerator damageFlash()
    {
        playerSprite.color = new Color(1, 0, 0, 0.5f);
        yield return new WaitForSeconds(changedColorTime);
        playerSprite.color = Color.white;
    }*/

}
