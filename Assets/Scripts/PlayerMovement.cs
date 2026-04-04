using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public float jumpingPower = 10f;
    public bool jumpAbility;

    private bool isFacingRight = true;
    private bool doubleJump;
    private float horizontal;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        DoJumping();
        
        Flip();
    }

    private void DoJumping()
    {
        // Реализация прыжков
        if (isGrounded() && !Input.GetButton("Jump") && jumpAbility)
        {
            doubleJump = false;
        }

        if (Input.GetButtonDown("Jump") && jumpAbility)
        {
            if (isGrounded() || doubleJump)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);

                doubleJump = !doubleJump;
            }
        }

        if (Input.GetButtonDown("Jump") && !jumpAbility)
        {
            if (isGrounded())
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            }
        }

        // Снижение высоты прыжков
        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        // Отзеркаливание спрайта в зависимости от напрвления
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
