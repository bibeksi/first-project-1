using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementRunner : MonoBehaviour
{
    public Transform model;

    [Header("Movement")]
    public float forwardSpeed = 6f;
    public float laneWidth = 2f;
    public float laneChangeSpeed = 12f;

    [Header("Jump")]
    public float jumpForce = 8f;
    public float gravity = -20f;

    private CharacterController controller;
    private int currentLane = 1; // 0 = left, 1 = middle, 2 = right
    private float verticalVelocity = 0f;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleLaneInput();
        HandleJump();

        // ⭐ Smooth slow speed increase over time (not rapid)
        forwardSpeed += 0.03f * Time.deltaTime;

        // ⭐ Limit max speed (so it doesn't get too fast)
        forwardSpeed = Mathf.Clamp(forwardSpeed, 6f, 14f);

        Move();
    }

    void HandleLaneInput()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            currentLane = Mathf.Max(0, currentLane - 1);

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            currentLane = Mathf.Min(2, currentLane + 1);
    }

    void HandleJump()
    {
        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                verticalVelocity = jumpForce;
            else
                verticalVelocity = -1f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
    }

    void Move()
    {
        // forward movement
        Vector3 move = Vector3.forward * forwardSpeed;
        move.y = verticalVelocity;

        // LANE MOVEMENT
        float targetX = (currentLane - 1) * laneWidth; // -2, 0, +2
        float newX = Mathf.Lerp(transform.position.x, targetX, Time.deltaTime * laneChangeSpeed);

        // Apply horizontal + forward
        controller.Move(new Vector3(newX - transform.position.x, 0, move.z * Time.deltaTime));

        // Apply vertical
        controller.Move(new Vector3(0, move.y * Time.deltaTime, 0));

        // Keep visual model centered on Player root
        if (model != null)
            model.localPosition = Vector3.zero;
    }
}
