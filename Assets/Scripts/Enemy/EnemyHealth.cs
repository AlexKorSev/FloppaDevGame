using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Enemy Health")]
    public float health;
    public float maxHealth;

    public float pointsToGive;
    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
        {
            ScoreManager.Instance.AddScore(pointsToGive);
            Destroy(gameObject);
        }
    }
}
