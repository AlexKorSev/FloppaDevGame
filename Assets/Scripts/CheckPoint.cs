using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    PlayerHealth playerHealth;
    public Transform respawnPoint;

    private SpriteRenderer spriteRend;
    private Collider2D coll;

    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        spriteRend = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth.UpdateCheckpoint(respawnPoint.position);
            spriteRend.color = Color.green;
            coll.enabled = false;
        }
    }
}
