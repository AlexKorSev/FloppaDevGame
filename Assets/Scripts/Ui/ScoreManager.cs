using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("Настройки Счета")]
    public float currentScore;
    public int collected;
    public int destroyed;

    private float gameTime = 0f;

    [Header("Counters")]
    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField] private TextMeshProUGUI timerLabel;

    //[Header("Звук")]
    //[SerializeField] private AudioSource audioSource;
    //[SerializeField] private AudioClip coinClip;

    private static float Level1Max = 500f;
    private static float Level2Max = 1900f;
    private static float Level3Max = 1750f;

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            collected = 0; destroyed = 0;
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
        //if (audioSource != null && coinClip != null)
        //{
        //    audioSource.PlayOneShot(coinClip);
        //}
    }

    public void AddCollected(int amount)
    {
        collected += amount;
    }

    public void AddDestroyed(int amount)
    {
        destroyed += amount;
    }

    public float GetGameTime()
    {
        return gameTime;
    }

    public static float GetMaxPoints(string levelName)
    {
        if (levelName == "Level1")
        {
            return Level1Max;
        }
        else if (levelName == "Level2")
        {
            return Level2Max;
        }
        else
        {
            return Level3Max;
        }
    }
}