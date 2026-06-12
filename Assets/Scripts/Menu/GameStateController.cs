using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{
    public static bool IsGamePaused { get; private set; }
    public static bool IsGameRunning { get; set; }

    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject nextLevelScreen;

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

        Destroy(playerStartPos);
        SceneManager.LoadScene("Menu");
    }
}