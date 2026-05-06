using UnityEngine;


public class EnemyMovement : MonoBehaviour
{

    public GameObject pointA;
    public GameObject pointB;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform currentPoint;
    [SerializeField] private float speed = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
    }

    //private void FixedUpdate()
    //{
    //    rb.linearVelocity = new Vector2(currentDirection * speed, rb.linearVelocity.y);
    //}

    private void Update()
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
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.25f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.25f);

    }
}
