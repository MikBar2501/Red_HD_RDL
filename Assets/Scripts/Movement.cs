﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (AudioSource))]
public class Movement : MonoBehaviour
{
    public Stats stats;

    private bool onSlope = false;
    private Rigidbody objectsRigidbody;
    private Vector3 movementDirection;
    private int layerMask = 0 << 0;
    private float characterHeight;
    private Animator animator;
    [SerializeField] private AudioClip[] m_FootstepSounds;
    [SerializeField] private float m_StepInterval;
    [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
    private AudioSource m_AudioSource;
    private float m_NextStep;
    private float m_StepCycle;


    private void Start() {
        m_AudioSource = GetComponent<AudioSource>();
        m_StepCycle = 0f;
        m_NextStep = m_StepCycle/2f;
    }

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
        bool walk;

        if (Input.GetButton("Sprint"))
        {
            actualSpeed *= stats.sprintMultiplayer;
            animator.SetBool("IsSprinting", true);
            walk = false;
        }
        else
        {
            animator.SetBool("IsSprinting", false);
        }

        if (!onSlope && movementDirection.magnitude > 0)
        {
            animator.SetBool("IsMoving", true);
            movement = movementDirection * actualSpeed * Time.deltaTime;
            walk = true;
            ProgressStepCycle(actualSpeed,walk);
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

    private void PlayFootStepAudio()
    {
        int n = Random.Range(1, m_FootstepSounds.Length);
        m_AudioSource.clip = m_FootstepSounds[n];
        m_AudioSource.PlayOneShot(m_AudioSource.clip);
        m_FootstepSounds[n] = m_FootstepSounds[0];
        m_FootstepSounds[0] = m_AudioSource.clip;
    }

    private void ProgressStepCycle(float speed, bool walk)
        {
            if (GetComponent<Rigidbody>().velocity.sqrMagnitude > 0)
            {
                m_StepCycle += (GetComponent<Rigidbody>().velocity.magnitude + (speed*(walk ? m_RunstepLenghten : 1f ))) * Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }

}
