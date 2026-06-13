using UnityEngine;

public class Coin : MonoBehaviour
{
    public float pointsToGive = 50f;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Вызываем метод в менеджере. 
            // Он добавит очки и проиграет звук 2D.
            ScoreManager.Instance.AddScore(pointsToGive);
            ScoreManager.Instance.AddCollected(1);
            audioManager.PlaySFX(audioManager.coin);

            Destroy(gameObject);
        }
    }
}