using System.Collections;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health")]
    public float health;
    public float maxHealth;
    public bool hit;

    [SerializeField] public Transform startPoint;

    [SerializeField] private Image healthBar;
    [SerializeField] private Image totalHealthBar;
    private float oneHearthAmount = 20f;
    private float maxHearths = 7f;

    [Header("IFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;

    private SpriteRenderer spriteRend;

    AudioManager audioManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHealth = health;
        spriteRend = GetComponent<SpriteRenderer>();
        
        totalHealthBar.fillAmount = health / (maxHearths * oneHearthAmount);

    }

    private void Awake()
    {
        startPoint = FindAnyObjectByType<PlayerStartPos>().transform;
        transform.position = startPoint.position;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = health / (maxHearths * oneHearthAmount);

        if (hit)
        {
            audioManager.PlaySFX(audioManager.damageTaken);
            StartCoroutine(Invincibility());
            hit = false;
        }
        if (health <= 0)
        {
            GameStateController.IsGameRunning = false;
            Destroy(gameObject);
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void UpdateCheckpoint(Transform point)
    {
        startPoint = point;
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
}
