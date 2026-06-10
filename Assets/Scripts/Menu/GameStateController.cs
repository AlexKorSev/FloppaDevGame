using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{
    public static bool IsGamePaused { get; private set; }

    [Header("UI Настройки")]
    [SerializeField] private GameObject pauseMenuPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused) ResumeGame();
            else PauseGame();
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

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        IsGamePaused = false;
        SceneManager.LoadScene("MainMenu");
    }
}