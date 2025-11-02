using UC;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 7.0f;
    [SerializeField] private UnityEngine.InputSystem.PlayerInput playerInput;
    [SerializeField, InputPlayer(nameof(playerInput))] private UC.InputControl input;

    private Rigidbody2D rb;
    private Vector2 moveInput;

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

    private void Awake()
    {
        input.playerInput = playerInput;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (UserInput.instance.dashPressed && !isDashing && dashCooldownTimer <= 0.0f) StartDash();

        if (isDashing)
        {
            dashTimeLeft -= Time.deltaTime;

            if (dashTimeLeft <= 0.0f) EndDash();
        }
        Debug.Log($"Dash Cooldown: {dashCooldownTimer}");
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
        moveInput = input.GetAxis2();

        moveInput.Normalize();

        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
    }

    private void Dash()
    {
        moveInput = input.GetAxis2();

        moveInput.Normalize();

        rb.linearVelocity = new Vector2(moveInput.x * dashSpeed, moveInput.y * dashSpeed);
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
