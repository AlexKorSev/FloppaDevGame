using UnityEngine;

public class PlayerStartPos : MonoBehaviour
{
    public static GameObject positionKeeper;

    private void Awake()
    {
        if (positionKeeper == null)
        {
            positionKeeper = gameObject;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
