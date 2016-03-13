using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public bool useController = false;
    public float speed = 4F;

    void Update()
    {
        if (Input.GetButtonDown("Run"))
            speed = 3.5F;
        else if (Input.GetButtonUp("Run"))
            speed = 4F;

        transform.position += (transform.forward * Input.GetAxis("Vertical")) / speed;
        transform.position += (transform.right * Input.GetAxis("Horizontal")) / speed;
    }
}