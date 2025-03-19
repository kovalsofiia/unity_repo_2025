using UnityEngine;

public class CycloidSphere : MonoBehaviour
{
    public float R = 2f; //radius
    public float speed = 1f; // speed
    private float theta = 0f; // angle
    private Vector3 initialPosition; // start position
    private bool isMoving = true; 

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (isMoving)
        {
            // Збільшення кута theta для руху по циклоїді
            theta += Time.deltaTime * speed;

            // Обчислення зміщення для x і y
            float x = R * (theta - Mathf.Sin(theta)); // зміщення по осі X
            float y = R * (1 - Mathf.Cos(theta)); // зміщення по осі Y

            // Оновлення позиції об'єкта з урахуванням початкової позиції
            transform.position = initialPosition + new Vector3(x, y, 0);
        }
    }

    // Викликається при зіткненні з коллайдером
    private void OnCollisionEnter(Collision collision)
    {
        // Якщо об'єкт торкається бар'єра, зупинити рух
        if (collision.gameObject.CompareTag("Wall"))
        {
            isMoving = false;
        }
    }
}
