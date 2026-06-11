using FMODUnity;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("References")]
    public CharacterController controller;

    [Header("FMOD")]
    public EventReference woodStep;
    public EventReference stoneStep;

    [Header("Footsteps")]
    public float walkInterval = 0.5f;
    public float sprintInterval = 0.28f;

    private float stepTimer;

    void Update()
    {
        Vector3 horizontalVelocity = new Vector3(
            controller.velocity.x,
            0,
            controller.velocity.z
        );

        bool isMoving = horizontalVelocity.magnitude > 0.1f;
        bool isGrounded = controller.isGrounded;

        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        float currentInterval = isSprinting ? sprintInterval : walkInterval;

        if (isMoving && isGrounded)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = currentInterval;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    void PlayFootstep()
    {
        string surface = GetSurface();

        if (surface == "Stone")
        {
            RuntimeManager.PlayOneShot(stoneStep, transform.position);
        }
        else
        {
            RuntimeManager.PlayOneShot(woodStep, transform.position);
        }
    }

    string GetSurface()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 2f))
        {
            return hit.collider.tag;
        }

        return "Wood";
    }
}