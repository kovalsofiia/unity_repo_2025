using UnityEngine;

public class Cylinder : MonoBehaviour
{
    public float a = 5f; // параметр астроїди (визначає розмір кривої)
    public float speed = 1f; // speed
    private float theta = 0f; // angle
    private Vector3 initialPosition; // start position

    void Start()
    {
        initialPosition = transform.position;
    }

    void FixedUpdate()
    {
        // Збільшення кута theta для обертання
        theta += Time.deltaTime * speed;

        // Обчислення радіусу для астроїди
        float r = a * Mathf.Pow(1 - Mathf.Cos(theta), 2f / 3f);

        // Обчислення зміщення x та y
        float x = r * Mathf.Pow(Mathf.Cos(theta),3);
        float y = r * Mathf.Pow(Mathf.Sin(theta),3);

        // Оновлення позиції об'єкта з урахуванням початкової позиції
        transform.position = initialPosition + new Vector3(x, y, 0);
    }
}
