using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 10f; // Швидкість руху вперед-назад
    public float horizontalSpeed = 10f; // Швидкість руху вліво-вправо
    public bool isGrounded = true; // Перевірка, чи герой на землі
    private bool gameStarted = false; 

    public Vector3 vectorA = new Vector3(1, 5, -7);
    public Vector3 vectorB = new Vector3(8, 0, -6);

    void Start()
    {
        Vector3 differenceVector = vectorA - vectorB;
        float squaredLength = differenceVector.sqrMagnitude;
        Debug.Log("Квадрат довжини різниці векторів: " + squaredLength);

        gameStarted = false;
    }

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

        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);


        // Рух вліво (A) і вправо (D)
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * horizontalSpeed * Time.deltaTime, Space.Self);

    }
    void OnCollisionEnter(Collision collision)
    {
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

    public void RestartGame()
    {
        gameStarted = false; // Скидаємо стан гри
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
