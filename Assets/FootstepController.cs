using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("References")]
    public CharacterController controller;

    [Header("FMOD")]
    public EventReference footstepEvent;

    [Header("Footsteps")]
    public float stepInterval = 0.5f;

    private float stepTimer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 horizontalVelocity =
            new Vector3(
                controller.velocity.x,
                0,
                controller.velocity.z
            );

        bool isMoving =
            horizontalVelocity.magnitude > 0.1f;

        bool isGrounded =
            controller.isGrounded;

        if (isMoving && isGrounded)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = stepInterval;
            }
        }
    }

    void PlayFootstep()
    {
        RuntimeManager.PlayOneShot(
            footstepEvent,
            transform.position
        );
    }
}
