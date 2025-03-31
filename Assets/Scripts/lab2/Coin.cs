using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public bool IsCollected { get; set; } = false;


    public float rotationSpeed = 100f; // Adjust this value to control the speed of rotation
    public float floatSpeed = 0.5f; // Adjust this value to control the speed of floating
    public float floatAmount = 0.1f; // Adjust this value to control the amount of float

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the coin around its local up axis (y-axis) with the specified speed
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Move the coin up and down using a sine wave
        Vector3 newPosition = startPosition + Vector3.up * Mathf.Sin(Time.time * floatSpeed) * floatAmount;
        transform.position = newPosition;
    }

}
