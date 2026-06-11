using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    PlayerHealth playerHealth;
    public Transform respawnPoint;

    private SpriteRenderer spriteRend;
    private Collider2D coll;

    [SerializeField] private Transform startPos;
    [SerializeField] private Sprite nextSprite;

    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        spriteRend = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        startPos = FindAnyObjectByType<PlayerStartPos>().transform;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //playerHealth.UpdateCheckpoint(respawnPoint);
            startPos.position = respawnPoint.position;
            
            spriteRend.sprite = nextSprite;

            coll.enabled = false;
        }
    }
}
