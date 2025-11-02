using UC;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 10f;
    private bool prevShootPressed = false;

    [SerializeField] private UnityEngine.InputSystem.PlayerInput playerInput;
    [SerializeField, InputPlayer(nameof(playerInput))] private UC.InputControl shootInput;


    private bool canFire;

    private void Awake()
    {
        shootInput.playerInput = playerInput;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        bool current = shootInput.IsDown();


        if (current && !prevShootPressed && canFire)
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
        bullet.GetComponent<BulletBehavior>().SetOwner(this.GetComponent<PlayerStats>());
    }
}
