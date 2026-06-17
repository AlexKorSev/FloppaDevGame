using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenuController : MonoBehaviour
{
    [SerializeField] public Image[] images;
    [SerializeField] public Sprite[] sprites;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Level1Result") &&
            PlayerPrefs.HasKey("Level2Result") &&
            PlayerPrefs.HasKey("Level3Result"))
        {
            LoadPrefs();
        }
        else
        {
            SetProgress();
        }
    }

    public void PlayGame(string level)
    {
        PlayerPrefs.SetInt("LevelCheck", 0);
        PlayerPrefs.SetInt("LevelPoints", 0);
        PlayerPrefs.SetInt("LevelTime", 0);
        PlayerPrefs.SetInt("LevelCollected", 0);
        PlayerPrefs.SetInt("LevelDestroyed", 0);
        SceneManager.LoadScene(level);
    }

    public void SetProgress()
    {
        PlayerPrefs.SetInt("Level1Result", 0);
        PlayerPrefs.SetInt("Level2Result", 0);
        PlayerPrefs.SetInt("Level3Result", 0);

        LoadPrefs();
    }

    public void LoadPrefs()
    {
        int level1 = PlayerPrefs.GetInt("Level1Result");
        int level2 = PlayerPrefs.GetInt("Level2Result");
        int level3 = PlayerPrefs.GetInt("Level3Result");

        images[0].sprite = sprites[level1];
        images[1].sprite = sprites[level2];
        images[2].sprite = sprites[level3];
    }

    private void OnDisable()
    {
        PlayerPrefs.Save();
    }

    public void QuitGame()
    {
        PlayerPrefs.Save();

        Application.Quit();
    }
}
