using UnityEngine;
using DG.Tweening;

public class MenuAnimator : MonoBehaviour
{
    public RectTransform[] buttons;
    public float duration = 0.5f;
    public float delayBetweenButtons = 0.15f;

    void OnEnable()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            RectTransform btn = buttons[i];
            CanvasGroup group = btn.GetComponent<CanvasGroup>();

            // 1. ВАЖНО: Запоминаем ТОТ масштаб, который ты выставил в инспекторе (1.5)
            Vector3 initialScale = btn.localScale;

            // 2. Сбрасываем в начальное состояние для анимации
            group.alpha = 0;
            // Начинаем, например, с 0.5 от твоего реального размера (т.е. с 0.75)
            btn.localScale = initialScale * 0.5f;

            btn.DOKill(); // Очистка старых анимаций

            // 3. Анимируем ПЕРЕМЕННУЮ initialScale (которая равна 1.5), а не единицу
            group.DOFade(1f, duration).SetDelay(i * delayBetweenButtons);

            btn.DOScale(initialScale, duration) // Теперь он вернется к твоим 1.5
               .SetDelay(i * delayBetweenButtons)
               .SetEase(Ease.OutBack);
        }
    }
}