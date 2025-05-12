using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public float rotationSpeed = 30f;
    public float rotationRange = 45f;

    private float initialRotation;

    void Start()
    {
        initialRotation = transform.rotation.eulerAngles.z;
    }

    void Update()
    {
        float angle = Mathf.Sin(Time.time * rotationSpeed) * rotationRange;
        transform.rotation = Quaternion.Euler(0f, 0f, initialRotation + angle);
    }
}