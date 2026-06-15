using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    PlayerHealth playerHealth;
    public Transform respawnPoint;

    private SpriteRenderer spriteRend;
    private Collider2D coll;

    [SerializeField] private Transform startPos;
    [SerializeField] private Sprite nextSprite;

    AudioManager audioManager;

    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        spriteRend = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();

        startPos = FindAnyObjectByType<PlayerStartPos>().transform;

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        startPos = FindAnyObjectByType<PlayerStartPos>().transform;
        if (other.CompareTag("Player"))
        {
            //playerHealth.UpdateCheckpoint(respawnPoint);
            audioManager.PlaySFX(audioManager.checkpoint);

            startPos.position = respawnPoint.position;
            spriteRend.sprite = nextSprite;

            coll.enabled = false;
        }
    }
}
