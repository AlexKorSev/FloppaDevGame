using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    [Header("Настройки")]
    public Image faderImage;
    public float fadeDuration = 1f;

    [SerializeField] private GameObject gameManager;


    private bool isTransitioning = false;
    private GameObject playerStartPos;

    private void Start()
    {
        if (faderImage != null)
        {
            // Блокируем переходы, пока идет анимация появления
            isTransitioning = true;
            faderImage.gameObject.SetActive(true);
            faderImage.color = new Color(0, 0, 0, 1);
            StartCoroutine(StartLevelFade());
        }
        playerStartPos = FindAnyObjectByType<PlayerStartPos>().gameObject;
    }

    private IEnumerator StartLevelFade()
    {
        // Плавное проявление
        yield return StartCoroutine(Fade(1, 0));
        // Разблокируем возможность перехода по флажку
        isTransitioning = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTransitioning)
        {
            Debug.Log("Контакт! Теперь всё сработает корректно.");

            isTransitioning = true;
            StartCoroutine(FadeToNextScene());
        }
    }

    private IEnumerator FadeToNextScene()
    {
        faderImage.gameObject.SetActive(true);

        // Затемнение
        yield return StartCoroutine(Fade(0, 0.5f));

        // Короткая пауза в полной темноте перед загрузкой
        yield return new WaitForSeconds(0.2f);

        gameManager.GetComponent<GameStateController>().CompleteLevel();

        //int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        //if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        //{
        //    SceneManager.LoadScene(nextSceneIndex);
        //}
        //else
        //{
        //    SceneManager.LoadScene(0);
        //}
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color color = faderImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            faderImage.color = new Color(color.r, color.g, color.b, newAlpha);
            yield return null;
        }

        faderImage.color = new Color(color.r, color.g, color.b, endAlpha);

        if (endAlpha <= 0.01f)
        {
            faderImage.gameObject.SetActive(false);
        }
    }
}