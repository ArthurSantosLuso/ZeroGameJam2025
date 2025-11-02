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
    [HideInInspector]
    public bool dashPressed;

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

        controls.Basics.Dash.performed += ctx => dashPressed = true;
        controls.Basics.Dash.canceled += ctx => dashPressed = false;
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    public static Vector2 SmoothMovement(Vector2 lastInput, Vector2 actualInput, float smoothSpeed)
    {
        return Vector2.Lerp(lastInput, actualInput, Time.deltaTime * smoothSpeed);
    } 

    public Vector2 GetSmoothAim() => aimSmooth;
}
