using TMPro;
using UnityEngine;

public class DisplayMessage : MonoBehaviour
{

    [Header("Message for player")]
    public string message;

    [SerializeField] public TextMeshProUGUI tmpObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            tmpObject.text = message;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        tmpObject.text = string.Empty;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    tmpObject.text = message;
    //}
}
