using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TerminalSequence : MonoBehaviour
{
    // Вложенный класс. Теперь Unity гарантированно не перепутает его с компонентом
    [System.Serializable]
    public class TerminalBlock
    {
        [TextArea(2, 5)]
        public string text;               // Сам текст
        public bool showLoadingBefore;    // Показывать ли "[LOADING DATA... OK]"
        public bool triggerGlitchAfter;   // Делать ли глитч-вспышку
        public float waitTimeAfter;       // Пауза после прочтения
    }

    [Header("UI Компоненты")]
    [SerializeField] private TMP_Text textComponent;
    [SerializeField] private Image glitchOverlay;

    [Header("Настройки звука")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip typingSound;
    [Range(0.5f, 1.5f)][SerializeField] private float minPitch = 0.85f;
    [Range(0.5f, 1.5f)][SerializeField] private float maxPitch = 1.15f;

    [Header("Настройки таймингов")]
    [SerializeField] private float typeSpeed = 0.03f;

    [Header("Настройки Глитча")]
    [SerializeField] private AudioClip glitchSound;
    [SerializeField] private string glitchChars = "!@#$%^&*()_+{}|[]<>?010101XYZ";

    [Header("Настройки перехода в следующую сцену")]
    [SerializeField] private string nextSceneName = "Menu";
    [SerializeField] private bool autoLoadNext = false;

    [Header("Сценарий терминала (Настрой в Инспекторе!)")]
    [SerializeField] private TerminalBlock[] sequenceBlocks;

    private bool sequenceFinished = false;

    void Start()
    {
        textComponent.text = "";
        if (glitchOverlay != null)
        {
            Color c = glitchOverlay.color;
            c.a = 0f;
            glitchOverlay.color = c;
        }

        StartCoroutine(PlaySequenceCoroutine());
    }

    void Update()
    {
        if (sequenceFinished && !autoLoadNext)
        {
            if (Input.anyKeyDown)
            {
                LoadNextScene();
            }
        }
    }

    IEnumerator PlaySequenceCoroutine()
    {
        textComponent.text = "_";
        yield return new WaitForSeconds(0.5f);
        textComponent.text = "";
        yield return new WaitForSeconds(0.5f);
        textComponent.text = "_";
        yield return new WaitForSeconds(0.5f);
        textComponent.text = "";

        for (int i = 0; i < sequenceBlocks.Length; i++)
        {
            var block = sequenceBlocks[i];

            if (block.showLoadingBefore)
            {
                yield return StartCoroutine(TypeText("\n\n[LOADING DATA... OK]\n"));
                yield return StartCoroutine(TriggerGlitchEffect(0.25f));
                yield return new WaitForSeconds(0.4f);
            }

            string prefix = (textComponent.text.Length > 0 && !block.showLoadingBefore) ? "\n\n" : "";
            yield return StartCoroutine(TypeText(prefix + block.text));

            yield return new WaitForSeconds(block.waitTimeAfter > 0 ? block.waitTimeAfter : 1.2f);

            if (block.triggerGlitchAfter)
            {
                yield return StartCoroutine(TriggerGlitchEffect(0.25f));
            }
        }

        sequenceFinished = true;

        if (autoLoadNext)
        {
            LoadNextScene();
        }
        else
        {
            yield return StartCoroutine(TypeText("\n\n[НАЖМИТЕ ЛЮБУЮ КНОПКУ ДЛЯ ПРОДОЛЖЕНИЯ]"));
        }
    }

    IEnumerator TypeText(string line)
    {
        foreach (char c in line)
        {
            textComponent.text += c;
            PlayTypingSound();
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    void PlayTypingSound()
    {
        if (audioSource != null && typingSound != null)
        {
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.PlayOneShot(typingSound);
        }
    }

    IEnumerator TriggerGlitchEffect(float duration)
    {
        if (audioSource != null && glitchSound != null)
        {
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(glitchSound);
        }

        Vector3 originalPosition = textComponent.rectTransform.localPosition;
        Color originalColor = textComponent.color;
        string currentText = textComponent.text;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-20f, 20f);
            float offsetY = Random.Range(-15f, 15f);
            textComponent.rectTransform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0);

            textComponent.color = Random.value > 0.5f ? Color.red : (Random.value > 0.3f ? Color.yellow : originalColor);

            string randomJibberish = "";
            for (int i = 0; i < 10; i++)
            {
                randomJibberish += glitchChars[Random.Range(0, glitchChars.Length)];
            }
            textComponent.text = currentText + randomJibberish;

            if (glitchOverlay != null)
            {
                Color overlayColor = Random.value > 0.5f ? Color.white : Color.black;
                overlayColor.a = Random.Range(0.1f, 0.5f);
                glitchOverlay.color = overlayColor;
            }

            elapsed += 0.04f;
            yield return new WaitForSeconds(0.04f);
        }

        textComponent.rectTransform.localPosition = originalPosition;
        textComponent.color = originalColor;
        textComponent.text = currentText;

        if (glitchOverlay != null)
        {
            Color c = glitchOverlay.color;
            c.a = 0f;
            glitchOverlay.color = c;
        }
    }

    void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Имя следующей сцены не указано!");
        }
    }
}