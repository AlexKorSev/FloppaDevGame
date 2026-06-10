using System.Collections;
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
    [SerializeField] private float jumpingPower = 6f;

    private bool ifPhase2;
    private bool walkBreak;
    private SpriteRenderer spriteRend;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    [Header("Transition")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;

    // Phase 2 additions
    [Header("Phase 2")]
    [SerializeField] private float jumpCooldownMin = 2f;   // min seconds between jumps
    [SerializeField] private float jumpCooldownMax = 5f;   // max seconds between jumps
    [SerializeField] private LayerMask groundLayer;        // assign "Ground" layer in Inspector
    [SerializeField] private Transform groundCheck;
    private float nextJumpTime;
    private float originalSpeed;     // to store starting speed for potential reset

    [SerializeField] private GameObject endGates;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        currentPoint = pointB.transform;
        ifPhase2 = false;
        walkBreak = false;

        maxHealth = health;
        originalSpeed = speed;       // store original speed
        //anim = GetComponent<Animator>();
        //anim.SetBool("isWalking", true);
    }

    private void Update()
    {
        ManageHealth();
        ManageBehavior();

        // Phase 2 random jump (only when walking, not during transition)
        if (ifPhase2 && !walkBreak && IsGrounded() && Time.time >= nextJumpTime)
        {
            Jump();
            nextJumpTime = Time.time + Random.Range(jumpCooldownMin, jumpCooldownMax);
        }
    }

    private void ManageBehavior()
    {
        if (!walkBreak)
        {
            // Determine movement direction
            float direction = (currentPoint == pointB.transform) ? 1f : -1f;
            // Move horizontally, keep vertical velocity (to allow jumping)
            rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

            // Patrol point switching
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

        // Phase 2 logic: jump handling is done in Update
        // (removed the incomplete ifPhase2 block from original)
    }

    private void ManageHealth()
    {
        if (health <= 0)
        {
            endGates.gameObject.SetActive(false);
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
        // Stop movement during transition
        rb.bodyType = RigidbodyType2D.Kinematic;
        walkBreak = true;
        rb.linearVelocity = Vector2.zero;   // ensure no lingering movement

        // Blink effect
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }

        // Activate phase 2
        ifPhase2 = true;
        speed = originalSpeed * 2f;    // double walking speed
        nextJumpTime = Time.time + Random.Range(0.5f, 1.5f); // first jump soon after transition

        // Resume movement
        rb.bodyType = RigidbodyType2D.Dynamic;
        walkBreak = false;
    }

    private void Jump()
    {
        // Apply upward force only if grounded (ensured by IsGrounded())
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
    }

    private bool IsGrounded()
    {
        //float rayLength = 0.1f;
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayer);

        //Debug.DrawRay(transform.position, Vector2.down * rayLength, Color.red);
        //return hit.collider != null;
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.25f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.25f);
    }
}