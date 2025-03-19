using UnityEngine;

public class RotatingRod : MonoBehaviour
{
    public float rotationSpeed = 100f;  // speed
    
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
