using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Вызываем метод в менеджере для добавления монетки
            ScoreManager.Instance.AddCoin(1); 
            Destroy(gameObject);
        }
    }
}