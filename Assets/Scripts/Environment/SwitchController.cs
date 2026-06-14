using UnityEngine;

public class SwitchController : MonoBehaviour
{

    private bool isPressed;
    private SpriteRenderer spriteRend;
    [SerializeField] private Sprite[] sprites;

    AudioManager audioManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPressed = false;
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed)
        {
            spriteRend.sprite = sprites[1];
        }
        else
        {
            spriteRend.sprite = sprites[0];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioManager.PlaySFX(audioManager.doorMove);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        isPressed = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPressed = false;
        spriteRend.color = Color.white;
    }

    public bool IsPressed()
    {
        return isPressed;
    }
}
