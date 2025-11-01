using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 7.0f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    //private bool willNormalize;

    private void Start()
    {
        //willNormalize = true;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();

        //if (Gamepad.current.buttonSouth.wasPressedThisFrame)
        //{
        //    willNormalize = !willNormalize;
        //    Debug.Log($"A foi pressionado, vai ser normalizado: {willNormalize}");
        //}
    }

    private void Move()
    {
        moveInput = UserInput.instance.moveInput;

        //if (willNormalize) 
        moveInput.Normalize();

        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
    }
}
