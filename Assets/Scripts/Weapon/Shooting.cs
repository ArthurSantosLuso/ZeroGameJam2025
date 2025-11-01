using System.Runtime.CompilerServices;
using TreeEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    private void Update()
    {
        if (Gamepad.current.rightTrigger.wasPressedThisFrame)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
    }
}
