using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime); // самознищення через 5 сек
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //Destroy(other.gameObject); // тимчасово: знищити ворога
            Destroy(gameObject); // знищити файербол
        }
        else
        {
            Destroy(gameObject); // знищити при будь-якому зіткненні
        }
    }
}
