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

    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameStarted = false;
        originalSpeed = speed;


        //визначаємо на старті аніматор
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
        //if (collision.gameObject.CompareTag("Obstacle"))
        //{
        //    RestartGame();
        //}

        if (collision.gameObject.CompareTag("FinishLine"))
        {
            animator.SetBool("isJumping", true); // Запускаємо анімацію стрибка
            StartCoroutine(FinishLevel());
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

    //використовуються для обробки подій

    //підписуємось на подію
    void OnEnable()
    {
        TakeDamageCollider.OnPlayerDead += HandleGameOver; // Підписуємося на подію
        GameTimer.OnTimerEnd += HandleGameOver;
    }

    //відписуємось на подію
    void OnDisable()
    {
        TakeDamageCollider.OnPlayerDead -= HandleGameOver; // Відписуємося від події
        GameTimer.OnTimerEnd -= HandleGameOver;
    }

    //обробник події
    void HandleGameOver()
    {
        animator.SetBool("isDead", true); // Запускаємо анімацію смерті
        StartCoroutine(RestartAfterDelay(2f)); // Запускаємо корутину з затримкою 2 секунди
    }

    IEnumerator RestartAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        RestartGame(); // Викликаємо перезапуск сцени
    }


    IEnumerator FinishLevel()
    {
        // Можна додати анімацію або звук тут
        animator.SetBool("isWinner", true); // Запускаємо анімацію
        Debug.Log("Level Completed!");
        yield return new WaitForSeconds(2f);
        RestartGame();
    }



}
