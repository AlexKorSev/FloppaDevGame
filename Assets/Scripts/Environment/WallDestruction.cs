using System.Collections;
using UnityEngine;

public class WallDestruction : MonoBehaviour
{
    [Header("Настройки префабов")]
    [SerializeField] private GameObject fracturedWallPrefab;

    [Header("Настройки взрыва")]
    [SerializeField] private float explosionForce = 5f; // Сила разлета осколков

    private bool _isDestroyed = false;

    // Этот метод вызовет скрипт Damage в момент удара.
    public void DestroyWall(Vector2 pushDirection)
    {
        if (_isDestroyed) return;
        _isDestroyed = true;

        StartCoroutine(DestroyAndSpawnRoutine(pushDirection));
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
            GameObject fracturedObj = Instantiate(fracturedWallPrefab, transform.position, transform.rotation);
            FracturedPiece[] pieces = fracturedObj.GetComponentsInChildren<FracturedPiece>();

            foreach (FracturedPiece piece in pieces)
            {
                piece.InitializeImpulse(pushDirection, explosionForce);
            }

            Destroy(fracturedObj, 10f);
        }
        else
        {
            Debug.LogError($"[{name}] Ошибка: Не назначен fracturedWallPrefab!");
        }

        Destroy(gameObject);
    }
}