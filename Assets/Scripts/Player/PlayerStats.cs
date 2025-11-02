using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    public string Name { get; private set; }
    public int Score { get; private set; }
    public int Health { get; private set; }

    private SpriteRenderer spriteRenderer;
    private Collider2D playerCollider;
    private float respawnDelay = 1.5f;
    private float invincibleTime = 2f;
    private bool isInvincible = false;

    [SerializeField] private SpriteRenderer sprite;

    private void Awake()
    {
        playerCollider = GetComponent<Collider2D>();

        GlobalManager.instance.playerCnt += 1;
        Name = "Player" + GlobalManager.instance.playerCnt;
        Score = 0;
        Health = 2;
    }

    public void ReduceHealth(out bool died)
    {
        if (!isInvincible)
        {
            Health--;
            if (Health <= 0)
            {
                Die();
                died = true;
                return;
            }
        }
        died = false;
    }

    private void Die()
    {
        ChangeScore(-2);

        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        spriteRenderer.enabled = false;
        playerCollider.enabled = false;
        GetComponent<Shooting>().enabled = false;

        yield return new WaitForSeconds(respawnDelay);

        Health = 2;
        GetComponent<Shooting>().enabled = true;
        spriteRenderer.enabled = true;
        playerCollider.enabled = true;

        yield return StartCoroutine(InvincibilityCoroutine());
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        float elapsed = 0f;
        bool visible = true;

        while (elapsed < invincibleTime)
        {
            visible = !visible;
            spriteRenderer.enabled = visible;
            yield return new WaitForSeconds(0.2f);
            elapsed += 0.2f;
        }

        spriteRenderer.enabled = true;
        isInvincible = false;
    }

    public void ChangeScore(int value)
    {
        Score = Mathf.Max(0, Score + value);
        Debug.Log($"{Name} score: {Score}");
    }

    public void SwitchScore(int value)
    {
        Score = value;
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }
}
