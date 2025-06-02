using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using System.Collections;

public class TakeDamageCollider : MonoBehaviour
{
    public HealthBar healthBar;

    public int maxHealth = 100;
    public int currentHealth;


    public static event Action OnPlayerDead;

    [SerializeField] private Text trapsText;
    [SerializeField] private int traps;

    private Renderer playerRenderer;
    private Color originalColor;
    private Coroutine flashCoroutine;

    // Додані змінні для налаштування миготіння
    [SerializeField] private float flashDuration = 0.5f; // Загальна тривалість миготіння
    [SerializeField] private float flashInterval = 0.1f; // Інтервал між зміною кольорів (чим менше, тим швидше миготіння)

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        traps = 0;
        PlayerPrefs.SetInt("traps", traps);
        UpdateTrapsText();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerRenderer = player.GetComponentInChildren<SkinnedMeshRenderer>();
            if (playerRenderer == null)
            {
                playerRenderer = player.GetComponentInChildren<Renderer>();
            }

            if (playerRenderer != null)
            {
                originalColor = playerRenderer.material.color;
            }

        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        // Зупиняємо попереднє миготіння, якщо воно є, і запускаємо нове
        if (playerRenderer != null)
        {
            if (flashCoroutine != null)
            {
                StopCoroutine(flashCoroutine);
            }
            flashCoroutine = StartCoroutine(FlashRedRepeatedly());
        }

        if (currentHealth <= 0)
        {
            OnPlayerDead?.Invoke();
        }
    }

    // Змінена корутина для багаторазового миготіння
    private IEnumerator FlashRedRepeatedly()
    {
        float endTime = Time.time + flashDuration; // Час, коли миготіння має закінчитися

        while (Time.time < endTime)
        {
            playerRenderer.material.color = Color.red;
            yield return new WaitForSeconds(flashInterval);
            playerRenderer.material.color = originalColor;
            yield return new WaitForSeconds(flashInterval);
        }

        // Переконатися, що колір повертається до оригінального після закінчення миготіння
        playerRenderer.material.color = originalColor;
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
    }
}