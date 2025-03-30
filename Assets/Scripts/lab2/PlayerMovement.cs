using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Швидкість руху
    public float horizontalSpeed = 3f; // Швидкість руху вліво-вправо


    public float jumpForce = 5f; // Сила стрибка
    public bool isGrounded = true; // Перевірка, чи герой на землі

    private float originalSpeed;
    public float boostSpeedMultiplier = 2f; // Множник швидкості прискорення
    public float boostDuration = 5f; // Тривалість прискорення
    private bool isBoosting = false;

    private bool gameStarted = false;

    [SerializeField] private Text coinsText;
    [SerializeField] private int coins;


    private Animator animator;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameStarted = false;
        originalSpeed = speed;

        coins = 0;
        PlayerPrefs.SetInt("coins", coins);
        UpdateCoinsText();
        coins = PlayerPrefs.GetInt("coins");

        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        // Перевіряємо, чи гра почалася
        if (!gameStarted)
        {
            // Якщо гра не почалася, чекаємо натискання W
            if (Input.GetKeyDown(KeyCode.W))
            {
                gameStarted = true; // Гра почалася
            }
            return; // Виходимо з Update, якщо гра не почалася
        }

        // Рух вперед (W) і назад (S)
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);

        // Рух вліво (A) і вправо (D)
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * horizontalSpeed * Time.deltaTime, Space.Self);


        // Стрибок
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            animator.SetBool("isJumping", true); // Запускаємо анімацію стрибка

        }


        // Прискорення LShift
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isBoosting)
        {
            StartCoroutine(Boost());
        }

        animator.SetBool("isGrounded", isGrounded); // Оновлюємо параметр isGrounded

    }
    void OnCollisionEnter(Collision collision)
    {
        //перевірка чи герой врізався в перешкоду
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            RestartGame();
        }

        if (collision.gameObject.CompareTag("FinishLine"))
        {
            RestartGame();
        }
    }

    void OnCollisionStay(Collision collision)
    {
        //перевірка чи герой на землі
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false); // Завершуємо анімацію стрибка

        }
    }

    IEnumerator Boost()
    {
        isBoosting = true;
        animator.SetBool("isBoosting", true); // Запускаємо анімацію прискорення

        speed *= boostSpeedMultiplier; // Прискорюємо

        yield return new WaitForSeconds(boostDuration);

        speed = originalSpeed; // Повертаємо початкову швидкість
        isBoosting = false;
        animator.SetBool("isBoosting", false); // Завершуємо анімацію прискорення

    }

    public void RestartGame()
    {
        gameStarted = false; // Скидаємо стан гри
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
                Destroy(other.gameObject);
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
            Debug.LogError("CoinsText is not assigned in PlayerMovement script!");
        }
    }


    void OnEnable()
    {
        TakeDamageCollider.OnGameOver += HandleGameOver; // Підписуємося на подію
    }

    void OnDisable()
    {
        TakeDamageCollider.OnGameOver -= HandleGameOver; // Відписуємося від події
    }

    void HandleGameOver()
    {
        animator.SetBool("isDead", true); // Запускаємо анімацію смерті
        RestartGame(); // Викликаємо RestartGame()
    }



}
