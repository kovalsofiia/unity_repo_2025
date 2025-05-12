using UnityEngine;

public class ColorChangeAnimatorController : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        // Отримуємо компонент Animator
        animator = GetComponent<Animator>();

        // оскільки Loop Time увімкнено, вона гратиме автоматичо
        animator.enabled = true;
    }

    void Update()
    {
        
    }
}
