using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    public float speed = 8f;        // Скорость полета пули
    public float lifetime = 4f;     // Сколько секунд пуля живет, если ни во что не попала
    private Vector2 moveDirection;

    void Start()
    {
        // На всякий случай уничтожаем пулю через X секунд
        Destroy(gameObject, lifetime);
    }

    // Этот метод вызывается из турели при создании пули
    public void SetDirection(Vector2 dir)
    {
        moveDirection = dir.normalized;

        // Поворачиваем спрайт пули по направлению полета (если нужно)
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Update()
    {
        // Двигаем пулю в пространстве
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Turret") || collision.CompareTag("Projectile"))
        {
            return;
        }

        // Ожидание 0.05 секунды, чтобы прочиен скрипты успели отработать
        Destroy(gameObject, 0.05f);
    }
}