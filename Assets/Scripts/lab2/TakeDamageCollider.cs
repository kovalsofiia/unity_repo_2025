using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
public class TakeDamageCollider : MonoBehaviour
{
    public HealthBar healthBar;

    public int maxHealth = 100;
    public int currentHealth;

    public static event Action OnGameOver; // Оголошуємо подію

    public Text gameOverText; // Додаємо публічну змінну

    public float gameDuration = 60f; // Тривалість гри (60 секунд)
    private float timeRemaining;
    public Text timerText;



    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        gameOverText.gameObject.SetActive(false);
        timeRemaining = gameDuration;
    }

    private void Update()
    {
        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            CheckGameOver();
        }
        if (timerText != null)
        {
            timerText.text = "Час: " + Mathf.RoundToInt(timeRemaining);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
        CheckGameOver();
    }

    void CheckGameOver()
    {
        if (currentHealth <= 0 || timeRemaining <= 0) 
        {
            Debug.Log("Game Over!");
            if (gameOverText != null)
            {
                gameOverText.text = "Гра закінчилась!"; // Встановлюємо текст
                gameOverText.gameObject.SetActive(true); // Показуємо текст
            }
            OnGameOver?.Invoke();
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
