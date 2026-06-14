using System.Collections;
using UnityEngine;

// Требуем наличия Rigidbody2D, чтобы не забыть его добавить
[RequireComponent(typeof(Rigidbody2D))]
public class FracturedPiece : MonoBehaviour
{
    [Header("Настройки исчезновения")]
    [SerializeField] private float lifeTime = 1.5f;
    [SerializeField] private float fadeDuration = 1f;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(FadeAndDestroyRoutine());
    }

    public void Explode(Vector2 projectileDirection, Vector2 impactPoint, float forceMagnitude)
    {
        // Вектор "отталкивания" от точки попадания до центра конкретного осколка
        Vector2 piecePosition = transform.position;
        Vector2 explosionDir = (piecePosition - impactPoint).normalized;

        // мешиваем направление снаряда и направление взрыва
        // Коэффициенты (например, 0.7 и 0.3) можно менять, чтобы настроить "конус" разлета
        Vector2 finalDirection = (projectileDirection * 0.6f + explosionDir * 0.4f).normalized;

        // Добавляем каплю рандома, чтобы не было идеально симметрично
        finalDirection.x += Random.Range(-0.1f, 0.1f);
        finalDirection.y += Random.Range(-0.1f, 0.1f);
        finalDirection = finalDirection.normalized;

        // Случайная сила
        float randomizedForce = forceMagnitude * Random.Range(0.7f, 1.3f);

        // Случайное закручивание осколка в воздухе
        float randomTorque = Random.Range(-15f, 15f);

        // Применяем физику
        _rb.AddForce(finalDirection * randomizedForce, ForceMode2D.Impulse);
        _rb.AddTorque(randomTorque, ForceMode2D.Impulse);
    }

    private IEnumerator FadeAndDestroyRoutine()
    {
        yield return new WaitForSeconds(lifeTime);

        if (_spriteRenderer == null) yield break;

        Color originalColor = _spriteRenderer.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            _spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        Destroy(gameObject);
    }
}