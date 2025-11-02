using UC;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 7.0f;
    [SerializeField] private UnityEngine.InputSystem.PlayerInput playerInput;
    [SerializeField, InputPlayer(nameof(playerInput))] private UC.InputControl moveInput;

    [SerializeField, InputPlayer(nameof(playerInput))] private UC.InputControl dashInput;

    private Rigidbody2D rb;
    private Vector2 movement;

    [Header("Dash Configurations")]
    [SerializeField]
    private float dashSpeed = 10.0f;
    [SerializeField]
    private float dashDuration = 0.2f;
    [SerializeField]
    private float dashCooldown = 1.5f;

    private float dashCooldownTimer;
    private float dashTimeLeft;
    private bool isDashing = false;

    private Vector2 lastMovementInput = Vector2.zero;

    private void Awake()
    {
        moveInput.playerInput = playerInput;
        dashInput.playerInput = playerInput;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (dashInput.IsDown() && !isDashing && dashCooldownTimer <= 0.0f) StartDash();

        if (isDashing)
        {
            dashTimeLeft -= Time.deltaTime;

            if (dashTimeLeft <= 0.0f) EndDash();
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            Dash();
        }
        else
        {
            Move();
        }
        if (dashCooldownTimer > 0.0f) dashCooldownTimer -= Time.deltaTime;
    }

    private void Move()
    {
        movement = UserInput.SmoothMovement(movement, moveInput.GetAxis2(), 100.0f);

        movement.Normalize();

        rb.linearVelocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);
    }

    private void Dash()
    {
        lastMovementInput = movement;

        movement = UserInput.SmoothMovement(lastMovementInput, moveInput.GetAxis2(), 100.0f);

        movement.Normalize();

        rb.linearVelocity = new Vector2(movement.x * dashSpeed, movement.y * dashSpeed);
    }


    private void StartDash()
    {
        isDashing = true;
        dashTimeLeft = dashDuration;
        dashCooldownTimer = dashCooldown;
    }

    private void EndDash()
    {
        isDashing = false;
        rb.linearVelocity = Vector2.zero;
    }
}
