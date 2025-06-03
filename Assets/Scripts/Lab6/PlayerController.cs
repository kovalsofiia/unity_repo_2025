using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public NavMeshAgent agent;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent <Animator>();

    }


    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray movePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(movePosition, out var hitInfo))
            {
                animator.SetBool("isRunning",true);
                agent.SetDestination(hitInfo.point);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
        }
        
    }
}
