using UnityEngine;
using UnityEngine.InputSystem; // novo sistema de input

public class AimBehavior : MonoBehaviour
{
    public Transform player;
    public Transform weapon;
    private float radius = 1.5f;

    void Update()
    {
        Vector2 aimInput = UserInput.instance.GetSmoothAim();

        if (aimInput.sqrMagnitude > 0.1f)
        {
            float angle = Mathf.Atan2(aimInput.y, aimInput.x) * Mathf.Rad2Deg;

            Vector3 offset = new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad),
                0f
            ) * radius;

            weapon.position = player.position + offset;
            weapon.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}