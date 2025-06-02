using System;
using UnityEngine;
using UnityEngine.UI;

public class TotalTimer : MonoBehaviour
{
    private float elapsedTime = 0f;
    public Text timerText;
    [SerializeField] private GameObject deathPanel;

    private bool isGameRunning = true;

    public static event Action OnTimerEnd;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (deathPanel != null)
            deathPanel.SetActive(false);
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

                if (deathPanel != null)
                    deathPanel.SetActive(false);
                {
                    deathPanel.gameObject.SetActive(true);
                    Debug.Log("Game Over\nTotal Time: " + Mathf.RoundToInt(elapsedTime) + "s");
                }
            }
        }
    }
}
