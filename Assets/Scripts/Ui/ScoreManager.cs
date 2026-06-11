using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("Настройки Монет")]
    // Монеты всегда целые
    public int coinsCollected = 0;

    [Header("Звук")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip coinClip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoin(int amount = 1)
    {
        coinsCollected += amount;

        // Проигрываем звук (2D)
        if (audioSource != null && coinClip != null)
        {
            audioSource.PlayOneShot(coinClip);
        }
    }
}