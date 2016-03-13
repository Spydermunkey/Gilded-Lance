using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public bool useController = false;
    public float speed = 7.5F;

    void Update()
    {
        transform.position += (transform.forward * Input.GetAxis("Vertical")) * speed;
        transform.position += (transform.right * Input.GetAxis("Horizontal")) * speed;
    }
}