using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Stats stats;

    private bool onSlope = false;
    private Rigidbody objectsRigidbody;
    private Vector3 movementDirection;
    private int layerMask = 0 << 0;
    private float characterHeight;
    private Animator animator;


    private void Awake()
    {
        objectsRigidbody = GetComponent<Rigidbody>();
        characterHeight = GetComponent<CapsuleCollider>().height/2;
        animator = GetComponent<Animator>();

    }
    private void OnEnable()
    {
        // When the tank is turned on, make sure it's not kinematic.
        objectsRigidbody.isKinematic = false;
        layerMask = ~layerMask;

        movementDirection = Vector3.zero;
    }

    private void OnDisable()
    {
        // When the tank is turned off, set it to kinematic so it stops moving.
        objectsRigidbody.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }
    private void FixedUpdate()
    {
        Move();
        Turn();
        checkForSlope();
    }
    private void Move()
    {
        float actualSpeed = stats.movingSpeed;
        Vector3 movement = new Vector3();

        if (Input.GetButton("Sprint"))
        {
            actualSpeed *= stats.sprintMultiplayer;
            animator.SetBool("IsSprinting", true);
        }
        else
        {
            animator.SetBool("IsSprinting", false);
        }

        if (!onSlope && movementDirection.magnitude > 0)
        {
            animator.SetBool("IsMoving", true);
            movement = movementDirection * actualSpeed * Time.deltaTime;
        }
        else
            animator.SetBool("IsMoving", false);

        // Apply this movement to the rigidbody's position.
        objectsRigidbody.MovePosition(objectsRigidbody.position + movement);
    }

    
    private void Turn()
    { 
        if(movementDirection.magnitude > 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementDirection), stats.rotationSpeed * Time.deltaTime);

    }

    public void checkForSlope()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position - new Vector3(0, characterHeight, 0), transform.TransformDirection(Vector3.down), out hit, 1f, layerMask))
        {
            Vector3 hitNormal = hit.normal;
            float angle = Vector3.Angle(Vector3.down, hitNormal);

          //  Debug.DrawRay(transform.position - new Vector3(0, characterHeight, 0), transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            if (angle < 145)
                onSlope = true;
            else
                onSlope = false;
        }
    }

}
