using UnityEngine;

public class SwitchController : MonoBehaviour
{

    private bool isPressed;
    private SpriteRenderer spriteRend;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPressed = false;
        spriteRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        isPressed = true;
        spriteRend.color = Color.aliceBlue;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPressed = false;
        spriteRend.color = new Color(0, 0, 225, 255);
    }

    public bool IsPressed()
    {
        return isPressed;
    }
}
