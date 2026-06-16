using UnityEngine;

public class PauseButton : MonoBehaviour
{
    private GameStateController gameController;

    private void Start()
    {
        gameController = FindAnyObjectByType<GameStateController>();
    }

    public void PauseController()
    {
        gameController.PauseGame();
    }
}
