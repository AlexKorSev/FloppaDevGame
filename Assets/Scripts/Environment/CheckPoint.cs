using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    PlayerHealth playerHealth;
    public Transform respawnPoint;

    private SpriteRenderer spriteRend;
    private Collider2D coll;

    [SerializeField] private Sprite nextSprite;
    [SerializeField] private int pointIndex;

    ScoreManager scoreManager;
    AudioManager audioManager;

    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        spriteRend = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();

        scoreManager = GameObject.FindAnyObjectByType<ScoreManager>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioManager.PlaySFX(audioManager.checkpoint);

            spriteRend.sprite = nextSprite;

            PlayerPrefs.SetInt("LevelCheck", pointIndex);
            PlayerPrefs.SetInt("LevelPoints", (int)scoreManager.currentScore);
            PlayerPrefs.SetInt("LevelTime", (int)scoreManager.GetGameTime());
            PlayerPrefs.SetInt("LevelCollected", scoreManager.collected);
            PlayerPrefs.SetInt("LevelDestroyed", scoreManager.destroyed);


            coll.enabled = false;
        }
    }
}
