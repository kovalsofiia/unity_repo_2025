using UnityEngine;
using UnityEngine.UI;
using System;

public class GameTimer : MonoBehaviour
{
    public float gameDuration = 60f;
    private float timeRemaining;
    public Text timerText;
    public Text gameOverText;

    public static event Action OnTimerEnd;

    void Start()
    {
        gameOverText.gameObject.SetActive(false);
        timeRemaining = gameDuration;
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            OnTimerEnd?.Invoke();
        }
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.RoundToInt(timeRemaining);
        }
    }
}