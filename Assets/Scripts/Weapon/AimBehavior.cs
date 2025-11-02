using UC;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking; // novo sistema de input

public class AimBehavior : MonoBehaviour
{
    public Transform player;
    public Transform weapon;
    private float radius = 1.5f;

    [SerializeField] private UnityEngine.InputSystem.PlayerInput playerInput;
    [SerializeField, InputPlayer(nameof(playerInput))] private UC.InputControl aimInput;

    Vector2 aim = Vector2.zero;

    private void Awake()
    {
        aimInput.playerInput = playerInput;
    }

    void Update()
    {
        aim = UserInput.SmoothMovement(aim, aimInput.GetAxis2(), 100.0f);


        if (aim.sqrMagnitude > 0.1f)
        {
            float angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;

            Vector3 offset = new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad),
                0f
            ) * radius;

            Vector3 rotation = Vector3.zero;

            weapon.position = player.position + offset;
            weapon.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}