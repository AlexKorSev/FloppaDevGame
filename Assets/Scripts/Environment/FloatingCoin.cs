using UnityEngine;

public class FloatingCoin : MonoBehaviour
{
    [Header("Базовые настройки")]
    public float amplitude = 0.2f;
    public float baseFrequency = 2f;

    [Header("Рандомизация")]
    public float speedVariation = 0.5f; // На сколько скорость может отличаться (+/-)

    private Vector3 startPos;
    private float randomOffset;
    private float randomDirection;
    private float finalFrequency;

    void Start()
    {
        startPos = transform.position;

        // Случайное смещение по времени (от 0 до 2π, полный цикл синуса)
        randomOffset = Random.Range(0f, Mathf.PI * 2);

        // Случайный выбор знака (стороны): 1 или -1
        randomDirection = Random.value > 0.5f ? 1f : -1f;

        // Случайная скорость в диапазоне [base - var, base + var]
        finalFrequency = baseFrequency + Random.Range(-speedVariation, speedVariation);
    }

    void Update()
    {
        // Итоговая формула
        float wave = Mathf.Sin(Time.time * finalFrequency + randomOffset);
        float newY = startPos.y + (wave * amplitude * randomDirection);

        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}