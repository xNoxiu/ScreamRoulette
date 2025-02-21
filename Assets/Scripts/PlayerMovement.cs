using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private AudioSource footstepAudioSource;

    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    private Vector3 velocity;
    private bool isGrounded;
    private bool isSliding;
    private bool isMoving;

    public Transform groundCheck; 
    public float groundDistance = 0.4f; 
    public LayerMask groundMask; 


    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);

        isMoving = moveX != 0 || moveZ != 0;

        if (isMoving && !footstepAudioSource.isPlaying)
        {
            footstepAudioSource.Play();
        }
        else if (!isMoving && footstepAudioSource.isPlaying)
        {
            footstepAudioSource.Stop(); 
        }

        if (IsOnSteepSlope(out Vector3 slopeNormal))
        {
            isSliding = true;
            Vector3 slideDirection = new Vector3(slopeNormal.x, -slopeNormal.y, slopeNormal.z) * 5f;
            controller.Move(slideDirection * Time.deltaTime);
        }
        else
        {
            isSliding = false;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    bool IsOnSteepSlope(out Vector3 slopeNormal)
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, controller.height / 2 + 0.3f, groundMask))
        {
            slopeNormal = hit.normal;
            float angle = Vector3.Angle(Vector3.up, slopeNormal);
            return angle > controller.slopeLimit;
        }
        slopeNormal = Vector3.zero;
        return false;
    }

}
