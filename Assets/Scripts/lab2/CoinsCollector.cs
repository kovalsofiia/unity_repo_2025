using UnityEngine;
using UnityEngine.UI;

public class CoinsCollector : MonoBehaviour
{
    [SerializeField] private Text coinsText;
    [SerializeField] private int coins;

    void Start()
    {
        coins = 0; 
        PlayerPrefs.SetInt("coins", coins); // Цей рядок скидає збережені монети до 0
        UpdateCoinsText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Coin coin = other.gameObject.GetComponent<Coin>();
            if (coin != null && !coin.IsCollected)
            {
                coin.IsCollected = true; // Позначимо монету як зібрану
                coins++;
                PlayerPrefs.SetInt("coins", coins);
                Debug.Log("Coins : " + coins);
                UpdateCoinsText();

                // Запускаємо анімацію підстрибування
                coin.PlayBounceAnimation();
                // Монета буде видалена після завершення анімації
            }
        }
    }

    private void UpdateCoinsText()
    {
        if (coinsText != null)
        {
            coinsText.text = "Coins : " + coins;
        }
        else
        {
            Debug.LogError("CoinsText is not assigned in CoinsCollector script!");
        }
    }
}