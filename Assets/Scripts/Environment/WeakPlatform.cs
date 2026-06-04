using System.Collections;
using UnityEngine;

public class WeakPlatform : MonoBehaviour
{
    public float breakDelay;
    public float turnOnDelay;

    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Break());
        }
    }

    private IEnumerator Break()
    {
        yield return new WaitForSeconds(breakDelay);

        spriteRenderer.sprite = sprites[1];
        boxCollider.enabled = false;

        yield return new WaitForSeconds(turnOnDelay);

        spriteRenderer.sprite = sprites[0];
        boxCollider.enabled = true;
        
    }
}
