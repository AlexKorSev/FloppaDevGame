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

    private Vector3 originalScale;
    private bool isHovering = false;
    private float timeHoverStarted;

    private TextMeshProUGUI buttonText; // Ссылка на компонент текста

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Вместо transform.localScale пишем 1.5 вручную
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

        // играем звук:
        if (audioSource != null && hoverSound != null)
        {
            audioSource.pitch = Random.Range(0.8f, 1.1f);
            // PlayOneShot лучше обычного Play, так как он не прерывает звук, 
            // если вы быстро водите мышкой туда-сюда
            audioSource.PlayOneShot(hoverSound, 0.8f);
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