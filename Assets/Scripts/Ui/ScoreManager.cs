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

    private static float[] LevelMax = new float[] { 600f, 1700f, 1850f };

    private static int[] LevelCollectedMax = new int[] { 9, 9, 10 };

    private static int[] LevelDestroyedMax = new int[] { 0, 11, 12 };

    private static float[] FastTime = new float[] { 100f, 225f, 275f };


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            collected = 0; destroyed = 0;
        }
        if (PlayerPrefs.HasKey("LevelPoints") && PlayerPrefs.HasKey("LevelCheck"))
        {
            currentScore = PlayerPrefs.GetInt("LevelPoints");
            gameTime = PlayerPrefs.GetInt("LevelTime");
            collected = PlayerPrefs.GetInt("LevelCollected");
            destroyed = PlayerPrefs.GetInt("LevelDestroyed");
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
            return LevelMax[0];
        }
        else if (levelName == "Level2")
        {
            return LevelMax[1];
        }
        else
        {
            return LevelMax[2];
        }
    }

    public static int GetMaxCollected(string levelName)
    {
        if (levelName == "Level1")
        {
            return LevelCollectedMax[0];
        }
        else if (levelName == "Level2")
        {
            return LevelCollectedMax[1];
        }
        else
        {
            return LevelCollectedMax[2];
        }
    }

    public static int GetMaxDestroyed(string levelName)
    {
        if (levelName == "Level1")
        {
            return LevelDestroyedMax[0];
        }
        else if (levelName == "Level2")
        {
            return LevelDestroyedMax[1];
        }
        else
        {
            return LevelDestroyedMax[2];
        }
    }

    public static float GetFastTime(string levelName)
    {
        if (levelName == "Level1")
        {
            return FastTime[0];
        }
        else if (levelName == "Level2")
        {
            return FastTime[1];
        }
        else
        {
            return FastTime[2];
        }
    }
}