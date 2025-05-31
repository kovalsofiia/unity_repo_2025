using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
public class TakeDamageCollider : MonoBehaviour
{
    public HealthBar healthBar;

    public int maxHealth = 100;
    public int currentHealth;

    public static event Action OnPlayerDead; // Оголошуємо подію

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void TakeDamage(int damage)
    {

        currentHealth -= damage;
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        if (currentHealth <= 0)
        {
            Debug.Log("Player Dead!");
            OnPlayerDead?.Invoke();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pastka") && gameObject.CompareTag("Player"))
        {
            TakeDamage(20);
        }
    }

}
