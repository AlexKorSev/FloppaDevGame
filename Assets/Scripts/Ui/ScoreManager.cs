using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("Настройки Счета")]
    public float currentScore = 12000f;
    public float decreaseRate = 10f;

    [Header("Звук")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip coinClip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Уменьшаем счет, только если игра НЕ на паузе
        if (!GameStateController.IsGamePaused && currentScore > 0)
        {
            currentScore -= decreaseRate * Time.deltaTime;
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