using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("Настройки Счета")]
    public float currentScore = 0f;
    public float gameTime = 0f;

    [Header("Counters")]
    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField] private TextMeshProUGUI timerLabel;

    [Header("Звук")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip coinClip;

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        // Проверяем, существует ли менеджер (чтобы не было ошибок)
        if (Instance != null)
        {
            // Обновляем текст на экране каждый кадр. 
            // "F0" округляет число до целого (без запятых)
            scoreLabel.text = ScoreManager.Instance.currentScore.ToString("F0");

            gameTime = gameTime + Time.deltaTime;
            timerLabel.text = gameTime.ToString("F0");
        }
        else
        {
            Debug.Log("Менеджер не обнаружен");
        }
    }

    public void AddScore(float amount)
    {
        currentScore += amount;

        // Проигрываем звук (2D)
        if (audioSource != null && coinClip != null)
        {
            audioSource.PlayOneShot(coinClip);
        }
    }
}