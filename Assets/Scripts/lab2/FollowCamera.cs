using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // Об'єкт, за яким слідкує камера (ваш куб гравця)
    public Vector3 offset; // Зміщення камери відносно гравця

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
