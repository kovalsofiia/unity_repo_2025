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

    [SerializeField] private Text trapsText; // Новий текст для перешкод
    [SerializeField] private int traps; // Лічильник перешкод

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        traps = 0;
        PlayerPrefs.SetInt("traps", traps);
        UpdateTrapsText();
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
            OnPlayerDead?.Invoke();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pastka") && gameObject.CompareTag("Player"))
        {
            TakeDamage(20);
            traps++;
            PlayerPrefs.SetInt("traps", traps);
            Debug.Log("Traps : " + traps);
            UpdateTrapsText();
        }
    }

    private void UpdateTrapsText()
    {
        if (trapsText != null)
        {
            trapsText.text = "Traps : " + traps;
        }
        else
        {
            Debug.LogError("TrapsText is not assigned!");
        }
    }

}
