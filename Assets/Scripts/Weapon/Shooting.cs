using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;

    private bool prevShootPressed = false;

    private void Update()
    {
        bool current = UserInput.instance.shootPressed;

        // Dispara somente quando passar de false para true
        if (current && !prevShootPressed)
        {
            Shoot();
        }

        prevShootPressed = current;
    }

    void Shoot()
    {
        Vector2 aim = UserInput.instance.GetSmoothAim();
        if (aim.sqrMagnitude < 0.1f) return;

        Vector2 shootDir = aim.normalized;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<BulletBehavior>().SetDirection(shootDir);
    }
}
