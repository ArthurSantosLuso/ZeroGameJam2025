using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private bool hasHit = false;

    private Vector3 mousePos;
    private Camera mainCamera;
    private Rigidbody2D rb;
    [SerializeField] private float force;
    [SerializeField] private int damage;

    [SerializeField] private float maxLifeTime = 0.5f;
    private float timer;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
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

    public void IncreaseDamage(int amount)
    {
        damage += amount;
    }
}
