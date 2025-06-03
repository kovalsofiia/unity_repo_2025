using UnityEngine;

public class FireballShooter : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float fireballSpeed = 20f;
    public Animator animator;

    private float attackAnimationTime = 0.5f; // тривалість анімації

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryShoot();
        }
    }

    void TryShoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Запускаємо анімацію
        animator.SetBool("isFighting", true);
        Invoke(nameof(ResetIsFighting), attackAnimationTime);

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit))
        {
            // Якщо клік по ворогу
            if (hit.collider.CompareTag("Enemy"))
            {
                targetPoint = hit.collider.transform.position;
            }
            else
            {
                // Клік по землі або іншій поверхні
                targetPoint = hit.point;
            }

            // Обчислюємо напрямок
            Vector3 direction = (targetPoint - firePoint.position).normalized;

            // Створюємо файербол
            GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.LookRotation(direction));
            Rigidbody rb = fireball.GetComponent<Rigidbody>();
            rb.linearVelocity = direction * fireballSpeed;

            ParticleSystem ps = fireball.GetComponentInChildren<ParticleSystem>();
            if (ps != null)
            {
                ps.Play();
            }
        }
    }

    void ResetIsFighting()
    {
        animator.SetBool("isFighting", false);
    }
}
