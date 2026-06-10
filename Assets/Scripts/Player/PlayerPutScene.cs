using UnityEngine;

public class PlayerPutScene : MonoBehaviour
{
    private Vector2 checkPoint;
    private PlayerMovement playerMovement;

    public void StartReloading(Vector2 checkPoint)
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
}
