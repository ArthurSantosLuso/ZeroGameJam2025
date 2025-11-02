using UC;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Shooting : MonoBehaviour
{

    [SerializeField] private float bulletSpeed = 10f;
    private bool prevShootPressed = false;

    [SerializeField] private UnityEngine.InputSystem.PlayerInput playerInput;
    [SerializeField, InputPlayer(nameof(playerInput))] private UC.InputControl shootInput;

    [Header("Shot Proprieties")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private bool canFire;
    [SerializeField] private float timeBetweenFiring;
    private float timer;

    [SerializeField] private int maxAmmo;
    [SerializeField] private int currentAmmo;
    [SerializeField] private float reloadTime;
    private float reloadTimer;

    private bool isReloading = false;
    private float reloardStartDelay = 0.5f;
    private float idleTimer = 0f;
    public WeaponType Weapon { get; private set; }

    public enum WeaponType { Regular, Ghost, }

    private void Awake()
    {
        Weapon = WeaponType.Regular;
        shootInput.playerInput = playerInput;
    }

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    private void Update()
    {
        bool current = shootInput.IsDown();

        // Controlar o fire rate da arma
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }


        if (current && !prevShootPressed && canFire)
        {
            if (currentAmmo > 0)
            {
                canFire = false;
                Shoot();
                //shootAudioSource.PlayOneShot(shootSound);
                currentAmmo--;

                if (isReloading)
                {
                    isReloading = false;
                    //reloadAudioSource.Stop();
                    //reloadBarContainer.SetActive(false);
                    //reloadBarFill.fillAmount = 0f;
                }

                // Voltar o valor temporizador de recarga a zero caso o jogador volte a disparar enquanto recarrega
                reloadTimer = 0;
                idleTimer = 0;
            }

        }
        // Arma é recarregada automaticamente quando a quantidade de munição é zero
        // ou se for maior que zero quando o player não esta a disparar
        else
        {
            idleTimer += Time.deltaTime;

            if (currentAmmo < maxAmmo && idleTimer >= reloardStartDelay)
            {
                reloadTimer += Time.deltaTime;
                //reloadBarFill.fillAmount = Mathf.Clamp01(reloadTimer / reloadTime);

                if (!isReloading)
                {
                    //reloadAudioSource.pitch = reloadSound.length / reloadTime;
                    //reloadAudioSource.PlayOneShot(reloadSound);
                    isReloading = true;

                    //reloadBarContainer.SetActive(true);
                    //reloadBarFill.fillAmount = 0f;
                }
                if (reloadTimer > reloadTime)
                {
                    currentAmmo = maxAmmo;
                    reloadTimer = 0;    
                    isReloading = false;
                    //reloadAudioSource.pitch = 1.0f;

                    //reloadBarContainer.SetActive(false);
                    //reloadBarFill.fillAmount = 0f;
                }
            }
        }
        prevShootPressed = current;
    }

    private void Shoot()
    {
        Vector2 shootDir = firePoint.right;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<BulletBehavior>().SetDirection(shootDir);
        bullet.GetComponent<BulletBehavior>().SetOwner(gameObject);
    }

    public void ChangeWeaponType()
    {
        if (Weapon == WeaponType.Regular)
            Weapon = WeaponType.Ghost;
        else
            Weapon = WeaponType.Regular;
    }
}
