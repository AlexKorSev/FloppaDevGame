using System.Collections;
using UnityEngine;

public class WallDestruction : MonoBehaviour
{
    [Header("Настройки префабов")]
    [SerializeField] private GameObject fracturedWallPrefab;

    [Header("Настройки взрыва")]
    [SerializeField] private float explosionForce = 5f; // Сила разлета осколков

    private bool _isDestroyed = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_isDestroyed)
        {
            _isDestroyed = true;

            // Определяем направление импульса на основе контакта
            // Если точка контакта справа от центра игрока -> импульс идет вправо
            Vector2 hitDirection = Vector2.right;

            if (collision.contacts.Length > 0)
            {
                // Сравниваем позицию стены и позицию игрока
                float relativeX = transform.position.x - collision.transform.position.x;
                hitDirection = relativeX > 0 ? Vector2.right : Vector2.left;
            }

            StartCoroutine(DestroyAndSpawnRoutine(hitDirection));
        }
    }

    private IEnumerator DestroyAndSpawnRoutine(Vector2 pushDirection)
    {
        GetComponent<Collider2D>().enabled = false;

        if (TryGetComponent<SpriteRenderer>(out var spriteRenderer))
        {
            spriteRenderer.enabled = false;
        }

        yield return new WaitForSeconds(0.02f);

        if (fracturedWallPrefab != null)
        {
            // Спавним контейнер с осколками
            GameObject fracturedObj = Instantiate(fracturedWallPrefab, transform.position, transform.rotation);

            // Ищем все скрипты FracturedPiece у дочерних осколков
            FracturedPiece[] pieces = fracturedObj.GetComponentsInChildren<FracturedPiece>();

            // Передаем импульс каждому осколку
            foreach (FracturedPiece piece in pieces)
            {
                piece.InitializeImpulse(pushDirection, explosionForce);
            }

            // Сам контейнер (пустой объект) нам больше не нужен, 
            // так как дочерние осколки сами себя уничтожат через скрипт.
            // Но чтобы сцена не захламлялась пустышками, удалим контейнер чуть позже
            Destroy(fracturedObj, 10f);
        }
        else
        {
            Debug.LogError($"[{name}] Ошибка: Не назначен fracturedWallPrefab!");
        }

        Destroy(gameObject);
    }
}