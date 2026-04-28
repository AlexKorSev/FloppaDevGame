using UnityEngine;

public class PlayerGrabbing : MonoBehaviour
{

    public Transform firePoint;
    public Transform boxHolder;
    public float rayDist;

    private GameObject grabbedObject;

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D rayCheck = Physics2D.Raycast(firePoint.position, Vector2.right, rayDist);
        if (rayCheck.collider != null && rayCheck.collider.tag == "Box")
        {
            if (Input.GetButton("Fire2") && grabbedObject == null)
            {
                grabbedObject = rayCheck.collider.gameObject;
                grabbedObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                grabbedObject.transform.position = boxHolder.position;
                grabbedObject.transform.SetParent(transform);
                //rayCheck.collider.gameObject.transform.parent = boxHolder;
                //rayCheck.collider.gameObject.transform.position = boxHolder.position;
                //rayCheck.collider.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            }
            else if (Input.GetButton("Fire2"))
            {
                grabbedObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                grabbedObject.transform.SetParent(null);
                grabbedObject = null;
                //rayCheck.collider.gameObject.transform.parent = null;
                //rayCheck.collider.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
        }
        Debug.DrawRay(firePoint.position, transform.right * rayDist);
    }
}
