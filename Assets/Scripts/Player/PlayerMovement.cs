using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Stats")]
    public float speed = 6f;
    public float jumpingPower = 8f;
    public bool jumpAbility;
    public bool attackAbility;
    
    private bool isFacingRight = true;
    private bool doubleJump;
    private float horizontal;
    private Animator anim;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    //[SerializeField] private AudioSource jumpSound;

    public bool isOnPlatform;
    public Rigidbody2D platformRb;

    AudioManager audioManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        DoJumping();
        
        Flip();

        if (horizontal != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        anim.SetBool("isJumping", !isGrounded());
    }

    private void DoJumping()
    {
        if (isGrounded() && !Input.GetButton("Jump") && jumpAbility)
        {
            doubleJump = false;
        }

        if (Input.GetButtonDown("Jump") && jumpAbility)
        {
            if (isGrounded() || doubleJump)
            {
                if (isOnPlatform)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower * 1.3f + platformRb.linearVelocity.y * 0.5f);
                }
                else
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
                }
                doubleJump = !doubleJump;

                PlayJumpSound();
            }
        }

        if (Input.GetButtonDown("Jump") && !jumpAbility)
        {
            if (isGrounded())
            {
                if (isOnPlatform)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower * 1.3f + platformRb.linearVelocity.y * 0.5f);
                }
                else
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
                }
                
                PlayJumpSound();
            }
        }

        // Снижение высоты прыжков
        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.4f);
        }
    }

    private void FixedUpdate()
    {
        if (isOnPlatform)
        {
            rb.linearVelocity = new Vector2(horizontal * speed + platformRb.linearVelocity.x, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public bool canAttack()
    {
        return horizontal == 0 && attackAbility;
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

    private void PlayJumpSound()
    {
        //if (jumpSound != null)
        //{
        //    jumpSound.pitch = Random.Range(0.85f, 1.15f); // Тот самый разный звук
        //    jumpSound.PlayOneShot(jumpSound.clip);
        //}
        audioManager.PlaySFX(audioManager.jump);
    }
}
