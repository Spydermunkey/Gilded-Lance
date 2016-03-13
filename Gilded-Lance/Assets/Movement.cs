using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public bool useController = false;
    public float speed = 7.5F;

    void Update()
    {
        transform.position += (transform.forward * Input.GetAxis("Zed")) * speed;
        transform.position += (transform.right * Input.GetAxis("Ecks")) * speed;
    }
}