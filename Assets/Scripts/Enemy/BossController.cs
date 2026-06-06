using System.Collections;
using UnityEditor;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;

    [Header("Boss Health")]
    public float health;
    public float maxHealth;

    [SerializeField] private Transform currentPoint;
    [SerializeField] private float speed = 4f;

    private bool ifPhase2;
    private SpriteRenderer spriteRend;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    [Header("Transition")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        currentPoint = pointB.transform;
        ifPhase2 = false;

        maxHealth = health;
        //anim = GetComponent<Animator>();
        //anim.SetBool("isWalking", true);
    }

    private void Update()
    {
        ManageHealth();

        ManageBehavior();
    }

    private void ManageBehavior()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            rb.linearVelocity = new Vector2(speed, 0);
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            Flip();
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            Flip();
            currentPoint = pointB.transform;
        }

        if (ifPhase2)
        {

        }
    }

    private void ManageHealth()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        if (health <= (maxHealth / 2) && !ifPhase2)
        {
            StartCoroutine(StartPhase2());
        }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private IEnumerator StartPhase2()
    {
        boxCollider.enabled = false;
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(10, 10, 10, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        ifPhase2 = true;
        boxCollider.enabled = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.25f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.25f);
    }
}
