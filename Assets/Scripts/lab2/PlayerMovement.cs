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

    private bool isDead = false; // Чи мертвий гравець

    [SerializeField] private GameObject gameComplete;


    void Start()
    {
        gameStarted = false;
        originalSpeed = speed;
        animator = GetComponent<Animator>();
        gameComplete.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isDead) return; // Блокуємо рух після смерті

        if (!gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                gameStarted = true;
            }
            return;
        }

        // Рух вперед
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);

        // Рух вліво/вправо
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * horizontalSpeed * Time.deltaTime, Space.Self);

        // Стрибок
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }

        // Прискорення
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isBoosting)
        {
            StartCoroutine(Boost());
        }

        animator.SetBool("isGrounded", isGrounded);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FinishLine"))
        {
            animator.SetBool("isJumping", true);
            StartCoroutine(FinishLevel());
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    IEnumerator Boost()
    {
        isBoosting = true;
        animator.SetBool("isBoosting", true);
        speed *= boostSpeedMultiplier;
        yield return new WaitForSeconds(boostDuration);
        speed = originalSpeed;
        isBoosting = false;
        animator.SetBool("isBoosting", false);
    }

    public void RestartGame()
    {
        gameStarted = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnEnable()
    {
        TakeDamageCollider.OnPlayerDead += HandleGameOver;
        GameTimer.OnTimerEnd += HandleGameOver;
    }

    void OnDisable()
    {
        TakeDamageCollider.OnPlayerDead -= HandleGameOver;
        GameTimer.OnTimerEnd -= HandleGameOver;
    }

    void HandleGameOver()
    {
        if (isDead) return; // Не повторюємо, якщо вже мертвий

        isDead = true;
        animator.SetBool("isDead", true);
        Debug.Log("You are dead!");
    }

    IEnumerator RestartAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        RestartGame();
    }

    IEnumerator FinishLevel()
    {
        animator.SetBool("isWinner", true);
        if (gameComplete != null)
        {
            gameComplete.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(2f);

    }
}
