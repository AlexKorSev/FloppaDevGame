using UnityEngine;

public class PlayerGrabbing : MonoBehaviour
{

    public Transform firePoint;
    public Transform boxHolder;
    public float rayDist;
    public bool keyPressed;

    private GameObject grabbedObject;

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D rayCheck = Physics2D.Raycast(firePoint.position, Vector2.right, rayDist);

        if (rayCheck.collider != null && rayCheck.collider.CompareTag("Box"))
        {
            if (Input.GetButton("Fire2") && grabbedObject == null)
            {
                if (!keyPressed)
                {
                    GrabBox(rayCheck);

                    keyPressed = true;
                }
            }
        }
        else if (Input.GetButton("Fire2") && grabbedObject != null)
        {
            if (!keyPressed)
            {
                LetGoBox();

                keyPressed = true;
            }
        }
        else
        {
            keyPressed = false;
        }
    }

    private void GrabBox(RaycastHit2D rayCheck)
    {
        grabbedObject = rayCheck.collider.gameObject;
        grabbedObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        grabbedObject.GetComponent<BoxCollider2D>().enabled = false;
        grabbedObject.transform.position = boxHolder.position;
        grabbedObject.transform.SetParent(transform);

        boxHolder.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void LetGoBox()
    {
        grabbedObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        grabbedObject.GetComponent<BoxCollider2D>().enabled = true;
        grabbedObject.transform.SetParent(null);
        grabbedObject = null;

        boxHolder.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
