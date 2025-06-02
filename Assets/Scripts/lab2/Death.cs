using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Death : MonoBehaviour
{
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private float delayBeforeShowingPanel = 2f; // 3 секунди

    private void OnEnable()
    {
        TakeDamageCollider.OnPlayerDead += OnPlayerDeath;
    }

    private void OnDisable()
    {
        TakeDamageCollider.OnPlayerDead -= OnPlayerDeath;
    }

    private void Start()
    {
        if (deathPanel != null)
            deathPanel.SetActive(false);
    }

    private void OnPlayerDeath()
    {
        StartCoroutine(ShowDeathPanelAfterDelay());
    }

    private IEnumerator ShowDeathPanelAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeShowingPanel);

        if (deathPanel != null)
        {
            deathPanel.SetActive(true);
            Time.timeScale = 0f; // Пауза гри
        }
    }

    // Викликати з кнопки
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
