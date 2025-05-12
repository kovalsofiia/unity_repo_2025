using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cop : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 2f;
    public LayerMask obstacleMask;

    private void Update()
    {
        if (IsTargetVisible())
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("You lost!"); 
    }

    private bool IsTargetVisible()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity, obstacleMask))
        {
            if (hit.collider.transform != target)
            {
                return false;
            }
        }
        return true;
    }
}
