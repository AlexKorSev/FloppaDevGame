using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] public float damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Метод для коллизии для врагов
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Damage by an enemy
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().health -= damage;
            collision.gameObject.GetComponent<PlayerHealth>().hit = true;
        }


        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().health -= damage;
        }
        
        if (collision.gameObject.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<BossController>().health -= damage;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector2 pushDirection = transform.lossyScale.x > 0 ? Vector2.right : Vector2.left;
            collision.gameObject.GetComponent<WallDestruction>().DestroyWall(pushDirection);
        }
    }

    // Метод для коллизии для триггеров
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("Player"))
        {
            PlayerHealth player = collision.GetComponent<PlayerHealth>();
            Debug.Log($"[DEBUG] Ссылка на скрипт здоровья: {player} | Значение урона: {damage}");
            if (player != null)
            {
                player.health -= damage;
                player.hit = true;
            }
        }

        //Damage by spikes
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().health -= damage;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().health -= damage;
        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<BossController>().health -= damage;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector2 pushDirection = transform.lossyScale.x > 0 ? Vector2.right : Vector2.left;
            collision.gameObject.GetComponent<WallDestruction>().DestroyWall(pushDirection);
        }
    }
}
