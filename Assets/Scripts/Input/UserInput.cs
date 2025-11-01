using JetBrains.Annotations;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    [HideInInspector]
    public static UserInput instance;
    [HideInInspector]
    public Controls controls;
    [HideInInspector]
    public Vector2 moveInput;
    [HideInInspector]
    public Vector2 aimInput;
    [HideInInspector] 
    public bool shootPressed;

    private Vector2 moveSmooth;
    private Vector2 aimSmooth;

    [SerializeField] 
    private float smoothSpeed = 7f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        controls = new Controls();

        controls.Basics.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Basics.Movement.canceled += ctx => moveInput = Vector2.zero;

        controls.Basics.Aim.performed += ctx => aimInput = ctx.ReadValue<Vector2>();
        controls.Basics.Aim.canceled += ctx => aimInput = Vector2.zero;

        controls.Basics.Shoot.performed += ctx => shootPressed = true;
        controls.Basics.Shoot.canceled += ctx => shootPressed = false;
    }

    private void Update()
    {
        moveSmooth = Vector2.Lerp(moveSmooth, moveInput, Time.deltaTime * smoothSpeed);
        aimSmooth = Vector2.Lerp(aimSmooth, aimInput, Time.deltaTime * smoothSpeed);
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();
    public Vector2 GetSmoothMove() => moveSmooth;
    public Vector2 GetSmoothAim() => aimSmooth;
}
