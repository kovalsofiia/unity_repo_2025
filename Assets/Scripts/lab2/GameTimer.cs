using UnityEngine;
using UnityEngine.UI;
using System;

public class GameTimer : MonoBehaviour
{
    public float gameDuration = 60f;
    private float timeRemaining;
    public Text timerText;

    public GameObject deathPanel; // 👈 Панель поразки

    public static event Action OnTimerEnd;

    private bool isGameOver = false;

    void Start()
    {
        timeRemaining = gameDuration;

        // Сховати панель при старті гри
        if (deathPanel != null)
        {
            deathPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (!isGameOver)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                isGameOver = true;

                // Виклик події завершення гри
                OnTimerEnd?.Invoke();

                // Показати панель смерті
                if (deathPanel != null)
                {
                    deathPanel.SetActive(true);
                }
            }

            if (timerText != null)
            {
                timerText.text = "Time: " + Mathf.RoundToInt(timeRemaining);
            }
        }
    }
}
