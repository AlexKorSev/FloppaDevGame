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
    }

    // Метод для коллизии для триггеров
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Damage by spikes
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().health -= damage;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().health -= damage;
        }
    }
}
