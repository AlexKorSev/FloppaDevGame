using System.Collections;
using UnityEngine;

public class FracturedPiece : MonoBehaviour
{
    [Header("Настройки исчезновения")]
    [SerializeField] private float lifeTime = 1f;       // Сколько секунд осколок лежит до начала затухания
    [SerializeField] private float fadeDuration = 1f;   // Сколько времени занимает само растворение

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // Как только осколок появился — запускаем таймер жизни и растворения
        StartCoroutine(FadeAndDestroyRoutine());
    }

    // Метод, который будет вызывать целая стена при физическом ударе
    public void InitializeImpulse(Vector2 forceDirection, float forceMagnitude)
    {
        if (TryGetComponent<Rigidbody2D>(out var rb))
        {
            // Случайный разброс по вертикали и силе, чтобы осколки летели хаотично и красиво
            Vector2 randomizedDirection = new Vector2(
                forceDirection.x,
                Random.Range(-0.5f, 0.5f)
            ).normalized;

            float randomizedForce = forceMagnitude * Random.Range(0.7f, 1.3f);

            // Применяем импульс мгновенно
            rb.AddForce(randomizedDirection * randomizedForce, ForceMode2D.Impulse);
        }
    }

    private IEnumerator FadeAndDestroyRoutine()
    {
        // Ждем пока осколок просто лежит
        yield return new WaitForSeconds(lifeTime);

        if (_spriteRenderer == null)
        {
            Destroy(gameObject);
            yield break;
        }

        Color originalColor = _spriteRenderer.color;
        float elapsedTime = 0f;

        // Плавно уменьшаем альфа-канал (прозрачность)
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            // Вычисляем текущую прозрачность от 1 до 0
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            _spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            yield return null; // Ждем следующего кадра
        }

        // Удаляем объект, когда он стал полностью невидимым
        Destroy(gameObject);
    }
}