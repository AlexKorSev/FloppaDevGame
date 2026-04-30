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

        if (rayCheck.collider != null && rayCheck.collider.tag == "Box")
        {
            if (Input.GetButton("Fire2") && grabbedObject == null)
            {
                if (!keyPressed)
                {
                    grabbedObject = rayCheck.collider.gameObject;
                    grabbedObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                    grabbedObject.transform.position = boxHolder.position;
                    grabbedObject.transform.SetParent(transform);

                    keyPressed = true;
                }
            }

            else if (Input.GetButton("Fire2"))
            {
                if (!keyPressed)
                {
                    grabbedObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    grabbedObject.transform.SetParent(null);
                    grabbedObject = null;

                    keyPressed = true;
                }
            }

            else
            {
                keyPressed = false;
            }
        }
    }
}
