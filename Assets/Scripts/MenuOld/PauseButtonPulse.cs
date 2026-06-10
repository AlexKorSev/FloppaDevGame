using UnityEngine;
using UnityEngine.EventSystems;
using TMPro; // Обязательно добавляем для работы с текстом

public class PauseButtonPulse : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Настройки пульсации")]
    public float pulseSpeed = 4f;
    public float maxScaleMultiplier = 1.15f;

    [Header("Настройки цвета")]
    // Яркий зеленый (примерно #2ECC71)
    public Color normalColor = new Color(0.18f, 0.8f, 0.44f, 1f);
    // Светло-салатовый для пульсации (примерно #A2FFB8)
    public Color highlightColor = new Color(0.63f, 1f, 0.72f, 1f);

    [Header("Звуковые эффекты")]
    public AudioClip hoverSound; // Сюда класть файл звука
    private AudioSource audioSource;
    public float soundCooldown = 0.1f; // Интервал между звуками
    private static float _lastSoundPlayTime; // Общая переменная для всех кнопок

    [Header("Защита от раннего звука")]
    public float ignoreInputDuration = 5f; // Сколько секунд игнорировать наведение
    private float scriptStartTime;

    private Vector3 originalScale;
    private bool isHovering = false;
    private float timeHoverStarted;

    private TextMeshProUGUI buttonText; // Ссылка на компонент текста

    void Start()
    {
        scriptStartTime = Time.unscaledTime;

        audioSource = GetComponent<AudioSource>();
        originalScale = transform.localScale;
        buttonText = GetComponentInChildren<TextMeshProUGUI>();

        if (buttonText != null)
        {
            buttonText.color = normalColor;
            buttonText.fontWeight = FontWeight.Bold;
        }
    }

    void Update()
    {
        if (isHovering && buttonText != null)
        {
            float localPulseTime = Time.unscaledTime - timeHoverStarted;

            // Получаем значение от 0 до 1 (синхронно с пульсацией)
            float t = (Mathf.Sin(localPulseTime * pulseSpeed) + 1f) / 2f;

            // Изменяем размер
            transform.localScale = originalScale * Mathf.Lerp(1f, maxScaleMultiplier, t);

            float tColor = (Mathf.Sin(localPulseTime * pulseSpeed * 0.75f) + 1f) / 2f;
            buttonText.color = Color.Lerp(normalColor, highlightColor, tColor);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;

        timeHoverStarted = Time.unscaledTime;

        if (Time.unscaledTime - scriptStartTime < ignoreInputDuration) return;

        if (Time.unscaledTime - _lastSoundPlayTime < soundCooldown) return;

        if (audioSource != null && hoverSound != null)
        {
            _lastSoundPlayTime = Time.unscaledTime;

            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(hoverSound, 0.4f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        transform.localScale = originalScale;

        if (buttonText != null)
        {
            buttonText.color = normalColor;
        }
    }
}