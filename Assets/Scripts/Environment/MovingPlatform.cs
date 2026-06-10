using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public bool activated;

    [SerializeField] private float speed = 3f;
    private Vector3 nextPosition;

    public PlayerMovement playerMovement;
    public Rigidbody2D playerRb;
    public Rigidbody2D rb;
    Vector3 moveDirection;

    private void Awake()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextPosition = pointB.position;
        DirectionCalculate();
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            if (Vector2.Distance(transform.position, pointA.position) < 0.5f)
            {
                nextPosition = pointB.position;
                DirectionCalculate();
            }
            if (Vector2.Distance(transform.position, pointB.position) < 0.5f)
            {
                nextPosition = pointA.position;
                DirectionCalculate();
            }
        }
    }

    private void FixedUpdate()
    {
        if (activated)
        {
            rb.linearVelocity = moveDirection * speed;
        }
        else
        {
            rb.linearVelocity = new Vector2(0, 0);
        }
    }

    private void DirectionCalculate()
    {
        moveDirection = (nextPosition - transform.position).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerMovement.isOnPlatform = true;
            playerMovement.platformRb = rb;
            playerRb.gravityScale *= 10;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerMovement.isOnPlatform = false;
            playerRb.gravityScale /= 10;
        }
    }
}
