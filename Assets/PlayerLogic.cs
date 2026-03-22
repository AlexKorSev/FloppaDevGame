using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveForce = 50f;   // сила движения
    public float maxSpeed = 6f;     // максимальная скорость по X
    public float jumpForce = 12f;   // сила прыжка

    private Rigidbody2D rb;
    private bool canJump = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!rb)
            Debug.LogError("Rigidbody2D не найден на объекте!");
    }

    private void Update()
    {
        // Горизонтальное движение через клавиши
        float moveInput = 0f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            moveInput = -1f;
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            moveInput = 1f;

        // Применяем силу всегда, но ограничиваем скорость после
        rb.AddForce(new Vector2(moveInput * moveForce, 0f), ForceMode2D.Force);

        // Ограничение скорости по X
        rb.linearVelocity = new Vector2(Mathf.Clamp(rb.linearVelocity.x, -maxSpeed, maxSpeed), rb.linearVelocity.y);

        // Прыжок один раз
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && canJump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;
        }
    }

    // Сброс возможности прыгнуть при касании земли
    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                canJump = true;
                break;
            }
        }
    }
}