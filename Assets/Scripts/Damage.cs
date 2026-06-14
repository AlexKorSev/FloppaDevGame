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
        Debug.Log($"[Damage Collision] Снаряд врезался в: {collision.gameObject.name}, Тег: {collision.gameObject.tag}");
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

        if (collision.gameObject.CompareTag("WeakWall"))
        {
            if (collision.gameObject.TryGetComponent<WallDestruction>(out var wall))
            {
                // Узнаем, куда летел снаряд
                Vector2 dir = Vector2.right;
                if (TryGetComponent<Rigidbody2D>(out var rb))
                {
                    dir = rb.linearVelocity.normalized;
                }

                // Вызываем разрушение, передавая точку удара
                wall.ReplaceWallAndExplode(dir, collision.GetContact(0).point);
            }
        }
    }

    // Метод для коллизии для триггеров
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"[Damage Trigger] Снаряд прошел через: {collision.gameObject.name}, Тег: {collision.gameObject.tag}");

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

        if (collision.gameObject.CompareTag("WeakWall"))
        {
            Debug.Log("Шаг 1: Тег WeakWall подтвержден.");

            if (collision.gameObject.TryGetComponent<WallDestruction>(out var wall))
            {
                Debug.Log("Шаг 2: Компонент WallDestruction найден! Запускаю взрыв...");

                Vector2 dir = Vector2.right;
                if (TryGetComponent<Rigidbody2D>(out var rb))
                {
                    dir = rb.linearVelocity.normalized;
                }

                wall.ReplaceWallAndExplode(dir, collision.ClosestPoint(transform.position));
            }
            else
            {
                // Если скрипта нет, выводим красную ошибку в консоль
                Debug.LogError($"Шаг 2 ПРОВАЛЕН: На объекте {collision.gameObject.name} висит тег WeakWall, но НЕТ скрипта WallDestruction!");
            }
        }
    }
}
