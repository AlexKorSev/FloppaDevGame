using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenuController : MonoBehaviour
{
    public void PlayGame(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
