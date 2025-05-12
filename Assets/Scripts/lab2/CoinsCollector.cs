using UnityEngine;
using UnityEngine.UI;

public class CoinsCollector : MonoBehaviour
{

    [SerializeField] private Text coinsText;
    [SerializeField] private int coins;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coins = 0;
        PlayerPrefs.SetInt("coins", coins);
        UpdateCoinsText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            // Перевіряємо, чи монета ще не оброблена
            Coin coin = other.gameObject.GetComponent<Coin>();
            if (coin != null && !coin.IsCollected)
            {
                coin.IsCollected = true; // Позначимо монету як зібрану
                coins++;
                PlayerPrefs.SetInt("coins", coins);
                Debug.Log("Coins : " + coins);
                UpdateCoinsText();
                //видаляється зібрана вже монета
                Destroy(other.gameObject);
            }
        }
    }

    //функція для оновлення тексту кількості монет. 
    private void UpdateCoinsText()
    {
        if (coinsText != null)
        {
            coinsText.text = "Coins : " + coins;
        }
        else
        {
            Debug.LogError("CoinsText is not assigned in PlayerMovement script!");
        }
    }

}
