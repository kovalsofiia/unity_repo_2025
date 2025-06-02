using System;
using UnityEngine;
using UnityEngine.UI;

public class TotalTimer : MonoBehaviour
{
    private float elapsedTime = 0f;
    public Text timerText;
    public Text gameOverText;

    private bool isGameRunning = true;

    public static event Action OnTimerEnd;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverText.gameObject.SetActive(false);
        elapsedTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameRunning)
        {
            elapsedTime += Time.deltaTime;

            if (timerText != null)
            {
                timerText.text = "Time: " + Mathf.RoundToInt(elapsedTime);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isGameRunning = false;
                OnTimerEnd?.Invoke();

                if (gameOverText != null)
                {
                    gameOverText.gameObject.SetActive(true);
                    gameOverText.text = "Game Over\nTotal Time: " + Mathf.RoundToInt(elapsedTime) + "s";
                }
            }
        }
    }
}
