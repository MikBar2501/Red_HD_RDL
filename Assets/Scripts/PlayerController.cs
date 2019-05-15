using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            animator.SetBool("IsMoving", true);
        else
            animator.SetBool("IsMoving", false);

        if(Input.GetAxis("Sprint") != 0)
            animator.SetBool("IsSprinting", true);
        else
            animator.SetBool("IsSprinting", false);

    }
}
