using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField] private GameObject ownSwitch;

    private BoxCollider2D gateCollider;
    private SpriteRenderer spriteRend;
    private bool isOpen;

    private void Start()
    {
        gateCollider = GetComponent<BoxCollider2D>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        isOpen = ownSwitch.GetComponent<SwitchController>().IsPressed();
        gateCollider.enabled = !isOpen;

        if (isOpen)
        {
            spriteRend.color = Color.clear;
        }
        if (!isOpen)
        {
            spriteRend.color = Color.purple;
        }
    }

    private void FixedUpdate()
    {
        
    }
}
