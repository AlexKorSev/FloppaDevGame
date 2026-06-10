using UnityEngine;
using DG.Tweening;

public class MenuAnimator : MonoBehaviour
{
    public RectTransform[] buttons;
    public float duration = 0.35f;
    public float delayBetweenButtons = 0.075f;
    public float startDelay = 1.0f; // Пауза перед появлением самой первой кнопки

    [Header("Звук появления")]
    public AudioClip appearSound; // Сюда вешаем звук появления
    private AudioSource audioSource;

    void Awake()
    {
        // Получаем AudioSource с того же объекта, где висит этот скрипт
        audioSource = GetComponent<AudioSource>();

        // Если AudioSource нет, добавим его программно, чтобы не было ошибок
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnEnable()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            RectTransform btn = buttons[i];
            CanvasGroup group = btn.GetComponent<CanvasGroup>();

            Vector3 initialScale = btn.localScale;

            group.alpha = 0;
            btn.localScale = initialScale * 0.5f;

            btn.DOKill();

            // Вычисляем общую задержку: стартовая пауза + очередь кнопки
            float totalDelay = startDelay + (i * delayBetweenButtons);

            // Применяем общую задержку
            group.DOFade(1f, duration).SetDelay(totalDelay);

            btn.DOScale(initialScale, duration)
               .SetDelay(totalDelay)
               .SetEase(Ease.OutBack)
               .OnStart(() =>
               {
                   PlayAppearSound();
               });
        }
    }

    void PlayAppearSound()
    {
        if (audioSource != null && appearSound != null)
        {
            // Используем PlayOneShot, чтобы звуки накладывались друг на друга при быстром появлении
            audioSource.PlayOneShot(appearSound, 0.15f);
        }
    }
}