using UnityEngine;

public class SwitchController : MonoBehaviour
{
    SpriteRenderer renderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        renderer.color = Color.aliceBlue;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        renderer.color = new Color(0, 0, 225, 255);
    }
}
