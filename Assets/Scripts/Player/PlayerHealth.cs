using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health")]
    public float health;
    public float maxHealth;
    public bool hit;

    private Vector2 checkPoint;
    [SerializeField] private Image healthBar;

    [Header("IFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;

    private SpriteRenderer spriteRend;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHealth = health;
        checkPoint = transform.position;
        spriteRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);

        if (hit)
        {
            StartCoroutine(Invincibility());
            hit = false;
        }
        if (health <= 0)
        {
            Eliminate();
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    void Eliminate()
    {
        StartCoroutine(Respawn(0.5f));
    }

    public void UpdateCheckpoint(Vector2 point)
    {
        checkPoint = point;
    }

    private IEnumerator Invincibility()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);
        // duration
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(10, 10, 10, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(7, 8, false);
    }

    private IEnumerator Respawn(float duration)
    {
        spriteRend.enabled = false;
        yield return new WaitForSeconds(duration);
        transform.position = checkPoint;
        health = maxHealth;
        spriteRend.enabled = true;
    }
}
