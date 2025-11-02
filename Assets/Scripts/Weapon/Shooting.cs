using UC;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    private Vector2 aim = Vector2.zero;
    private bool prevShootPressed = false;

    [SerializeField] private UnityEngine.InputSystem.PlayerInput playerInput;
    [SerializeField, InputPlayer(nameof(playerInput))] private UC.InputControl shootInput;

    private void Awake()
    {
        shootInput.playerInput = playerInput;
    }

    private void Update()
    {
        bool current = shootInput.IsDown();


        if (current && !prevShootPressed)
        {
            Shoot();
        }

        prevShootPressed = current;
    }

    void Shoot()
    {
        Vector2 shootDir = firePoint.right;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<BulletBehavior>().SetDirection(shootDir);
    }
}
