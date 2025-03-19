using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 


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

   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameStarted = false;
        originalSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        // Перевіряємо, чи гра почалася
        if (!gameStarted)
        {
            // Якщо гра не почалася, чекаємо натискання стрілки вперед
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                gameStarted = true; // Гра почалася
            }
            return; // Виходимо з Update, якщо гра не почалася
        }

        // Рух вперед відносно локальної осі Z об'єкта
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);

        // Рух вліво-вправо відносно локальної осі X об'єкта
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * horizontalSpeed * Time.deltaTime, Space.Self);


        // Стрибок
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }



        // Прискорення
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isBoosting)
        {
            StartCoroutine(Boost());
        }
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
        }
    }

    IEnumerator Boost()
    {
        isBoosting = true;
        speed *= boostSpeedMultiplier; // Прискорюємо

        yield return new WaitForSeconds(boostDuration);

        speed = originalSpeed; // Повертаємо початкову швидкість
        isBoosting = false;
    }

    void RestartGame()
    {
        gameStarted = false; // Скидаємо стан гри
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
