using UnityEngine;


public class Projectile : MonoBehaviour
{

    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifeTime;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    //private Animator anim;

    private void Awake()
    {
        //anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hit) return;

        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

        lifeTime += Time.deltaTime;
        if (lifeTime > 5) Deactivate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        Deactivate();
        //anim.SetTrigger("explode");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        Deactivate();
        //anim.SetTrigger("explode");
    }

    public void SetDirection(float newDirection)
    {
        lifeTime = 0;
        direction = newDirection;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScale = transform.localScale.x;
        if (Mathf.Sign(localScale) != newDirection)
        {
            localScale = -localScale;
        }

        transform.localScale = new Vector3(localScale, 
            transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
