using UnityEngine;
using UnityEngine.EventSystems;
using TMPro; // Обязательно добавляем для работы с текстом

public class ButtonPulse : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Настройки пульсации")]
    public float pulseSpeed = 4f;
    public float maxScaleMultiplier = 1.15f;

    [Header("Настройки цвета")]
    public Color normalColor = new Color(1f, 0.698f, 0f); // #FFB200 в формате RGBA
    public Color highlightColor = new Color(1f, 0.85f, 0.4f); // Более светлый вариант для блика

    [Header("Звуковые эффекты")]
    public AudioClip hoverSound; // Сюда класть файл звука
    private AudioSource audioSource;

    [Header("Защита от раннего звука")]
    public float ignoreInputDuration = 5f; // Сколько секунд игнорировать наведение
    private float scriptStartTime;

    private Vector3 originalScale;
    private bool isHovering = false;
    private float timeHoverStarted;

    private TextMeshProUGUI buttonText; // Ссылка на компонент текста

    void Start()
    {
        // Запоминаем время, когда скрипт начал работу
        scriptStartTime = Time.time;

        audioSource = GetComponent<AudioSource>();
        originalScale = new Vector3(1.5f, 1.5f, 1.5f);
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null) buttonText.color = normalColor;
    }

    void Update()
    {
        if (isHovering && buttonText != null)
        {
            float localPulseTime = Time.time - timeHoverStarted;

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
        timeHoverStarted = Time.time;

        // Если с момента старта прошло меньше n секунд — выходим и не играем звук
        if (Time.time - scriptStartTime < ignoreInputDuration)
        {
            return; 
        }

        // код звука
        if (audioSource != null && hoverSound != null)
        {
            audioSource.pitch = Random.Range(0.8f, 1.1f);
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