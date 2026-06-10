using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{
    public static bool IsGamePaused { get; private set; }
    public static bool IsGameRunning { get; set; }

    [Header("Пауза")]
    [SerializeField] private GameObject pauseMenuPanel;


    [SerializeField] private GameObject gameOverScreen;

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

    public void GameOver()
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        IsGamePaused = false;
        SceneManager.LoadScene("Menu");
    }
}