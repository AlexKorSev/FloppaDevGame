using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour
{
    public static bool IsGamePaused { get; private set; }
    public static bool IsGameRunning { get; set; }

    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject completeLevelScreen;
    [SerializeField] private GameObject oprionsScreen;

    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] public TextMeshProUGUI tmpObject;

    [SerializeField] private Image medalImagePlace;
    [SerializeField] private Sprite[] medalSprites;

    //private GameObject playerStartPos;
    

    private void Start()
    {
        IsGameRunning = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && IsGameRunning)
        {
            if (IsGamePaused) ResumeGame();
            else PauseGame();
        }
        if (!IsGameRunning)
        {
            GameOver();
        }
    }

    public void ResumeGame()
    {
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
            oprionsScreen.SetActive(false);
        }
            
        Time.timeScale = 1f;
        IsGamePaused = false;
    }

    public void PauseGame()
    {
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(true);
        }
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    public void CompleteLevel()
    {
        if (completeLevelScreen != null)
        {
            string sceneName = SceneManager.GetActiveScene().name;

            float playTime = scoreManager.GetGameTime();
            int fragments = scoreManager.collected;
            int destroys = scoreManager.destroyed;
            float finalResult = scoreManager.currentScore;

            float fastTime = ScoreManager.GetFastTime(sceneName);
            int maxDestroyed = ScoreManager.GetMaxDestroyed(sceneName);
            int maxCollected = ScoreManager.GetMaxCollected(sceneName);
            float maxPoints = ScoreManager.GetMaxPoints(sceneName);

            string firstSentence = "Результаты:\nВремя прохождения - " + playTime.ToString("F0") + " сек.";
            string bonusSentence = "";
            if (playTime <= fastTime)
            {
                finalResult += 150f;
                bonusSentence = "\nБонус за быстрое прохождение - 150";
            }
            string secondSentence = "\nСобрано фрагментов - " + fragments + " / " + maxCollected;
            string thirdSentence = "";
            if (maxDestroyed != 0)
            {
                thirdSentence = "\nУстранено программ - " + destroys + " / " + maxDestroyed;
            }
            string fourthSentence = "\n\nИтог - " + finalResult + " / " + maxPoints;

            tmpObject.text = firstSentence + bonusSentence + secondSentence + thirdSentence + fourthSentence;

            // Deciding and placing needed medal
            if (finalResult < maxPoints * 0.4)
            {
                medalImagePlace.sprite = medalSprites[0];

                UpdatePrefs(sceneName, 1);
            }
            else if (finalResult < maxPoints * 0.75)
            {
                medalImagePlace.sprite = medalSprites[1];

                UpdatePrefs(sceneName, 2);
            }
            else if (finalResult <= maxPoints || finalResult > maxPoints)
            {
                medalImagePlace.sprite = medalSprites[2];

                UpdatePrefs(sceneName, 3);
            }
            
            Time.timeScale = 0f;
            completeLevelScreen.SetActive(true);
        }
    }

    public void UpdatePrefs(string name, int index)
    {
        string prefName = name + "Result";
        int fairIndex = Mathf.Max(PlayerPrefs.GetInt(prefName), index);
        PlayerPrefs.SetInt(prefName, fairIndex);
    }

    public void GameOver()
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
    }

    public void NextLevel()
    {

        PlayerPrefs.SetInt("LevelCheck", 0);
        PlayerPrefs.SetInt("LevelPoints", 0);
        PlayerPrefs.SetInt("LevelTime", 0);
        PlayerPrefs.SetInt("LevelCollected", 0);
        PlayerPrefs.SetInt("LevelDestroyed", 0);

        Time.timeScale = 1f;

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void RetryLevel()
    {
        ResumeGame();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        IsGamePaused = false;

        PlayerPrefs.SetInt("LevelCheck", 0);
        PlayerPrefs.SetInt("LevelPoints", 0);
        PlayerPrefs.SetInt("LevelTime", 0);
        PlayerPrefs.SetInt("LevelCollected", 0);
        PlayerPrefs.SetInt("LevelDestroyed", 0);

        SceneManager.LoadScene("Menu");
    }
}