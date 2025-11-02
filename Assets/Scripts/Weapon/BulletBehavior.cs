using UnityEditor.ShaderGraph;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private bool hasHit = false;
    private Rigidbody2D rb;

    [SerializeField] private float force;
    [SerializeField] private int damage;

    [SerializeField] private float maxLifeTime = 0.5f;
    private float timer;
    private Vector2 shootDirection;

    private PlayerStats owner;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = shootDirection.normalized * force;

        float rot = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit) return;
        PlayerStats enemy = collision.GetComponent<PlayerStats>();

        if (enemy && enemy != owner)
        {
            bool died;
            hasHit = true;
            enemy.ReduceHealth(out died);

            if (died)
            {
                owner.ChangeScore(2);
            }

            DestroyBullet();
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= maxLifeTime)
        {
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    public void SetDirection(Vector2 dir)
    {
        shootDirection = dir;
    }

    public void SetOwner(PlayerStats shooter)
    {
        owner = shooter;
    }
}
