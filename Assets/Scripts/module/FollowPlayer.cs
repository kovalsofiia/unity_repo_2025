using UnityEngine;

public class FollowPlayer : MonoBehaviour
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
}
