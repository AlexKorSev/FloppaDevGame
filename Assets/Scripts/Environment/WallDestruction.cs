using UnityEngine;

public class WallDestruction : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private GameObject fracturedWallPrefab;
    [SerializeField] private float explosionForce = 10f;

    // Публичный метод, который теперь дергает Damage.cs
    public void ReplaceWallAndExplode(Vector2 direction, Vector2 hitPoint)
    {
        if (fracturedWallPrefab != null)
        {
            // Спавним куски
            GameObject fracturedObj = Instantiate(fracturedWallPrefab, transform.position, transform.rotation);
            FracturedPiece[] pieces = fracturedObj.GetComponentsInChildren<FracturedPiece>();

            // Запускаем каждый осколок
            foreach (var piece in pieces)
            {
                piece.Explode(direction, hitPoint, explosionForce);
            }
        }
        else
        {
            Debug.LogError($"[{name}] Ошибка: Не назначен префаб разрушенной стены!");
        }

        // Уничтожаем целую стену
        Destroy(gameObject);
    }
}