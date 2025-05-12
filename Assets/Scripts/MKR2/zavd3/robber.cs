using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robber : MonoBehaviour
{   
    public float moveSpeed = 10f;

    void Update()
    {
        if (gameObject.CompareTag("Player"))
        {

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}
