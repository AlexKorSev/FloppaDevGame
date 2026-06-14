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

    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] public TextMeshProUGUI tmpObject;

    [SerializeField] private Image medalImagePlace;
    [SerializeField] private Sprite[] medalSprites;

    private GameObject playerStartPos;
    

    private void Start()
    {
        IsGameRunning = true;
        playerStartPos = FindAnyObjectByType<PlayerStartPos>().gameObject;
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
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;
    }

    public void PauseGame()
    {
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    public void CompleteLevel()
    {
        if (completeLevelScreen != null)
        {
            float playTime = scoreManager.GetGameTime();
            int fragments = scoreManager.collected;
            int destroys = scoreManager.destroyed;
            float finalResult = scoreManager.currentScore;

            float maxPoints = ScoreManager.GetMaxPoints(SceneManager.GetActiveScene().name);

            tmpObject.text = "Результаты:\nВремя прохождения - " + playTime.ToString("F0") 
                + "\nСобрано фрагментов - " + fragments 
                + "\nУстранено программ - " + destroys 
                + "\n\nИтог - " + finalResult;

            // Deciding and placing needed medal
            if (finalResult < maxPoints * 0.4)
            {
                medalImagePlace.sprite = medalSprites[0];
            }
            else if (finalResult < maxPoints * 0.7)
            {
                medalImagePlace.sprite = medalSprites[1];
            }
            else if (finalResult <= maxPoints || finalResult > maxPoints)
            {
                medalImagePlace.sprite = medalSprites[2];
            }
            
            Time.timeScale = 0f;
            completeLevelScreen.SetActive(true);
        }
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
        Destroy(playerStartPos);
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

        Destroy(playerStartPos);
        SceneManager.LoadScene("Menu");
    }
}