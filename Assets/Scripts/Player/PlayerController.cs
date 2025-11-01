using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7.0f;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    //private PlayerInput playerInput;
    //private Controls controls;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //playerInput = GetComponent<PlayerInput>();

        //controls = new Controls();

        // Liga o esquema de controle só deste jogador
        //controls.Enable();

        // Configura callbacks locais
        //controls.Basics.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        //controls.Basics.Movement.canceled += ctx => moveInput = Vector2.zero;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        moveInput = UserInput.instance.GetSmoothMove();

        moveInput.Normalize();

        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
    }
}
