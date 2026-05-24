using Unity.VisualScripting;
using UnityEngine;

public class PlatformActivator : MonoBehaviour
{
    [Header("Deactivator or Activator")]
    public bool activateAction;

    [SerializeField] private GameObject movingPlatform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activateAction && collision.gameObject.CompareTag("Player"))
        {
            movingPlatform.GetComponent<MovingPlatform>().activated = true;
        }
        else if (!activateAction && collision.gameObject.CompareTag("Player"))
        {
            movingPlatform.GetComponent<MovingPlatform>().activated = false;
        }
    }
}
