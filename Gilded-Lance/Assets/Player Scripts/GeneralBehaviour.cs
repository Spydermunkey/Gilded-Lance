using UnityEngine;
using System.Collections;

public class GeneralBehaviour : MonoBehaviour
{
    public Vector2 lookSensitivity = new Vector2(5, 5);

    private Transform pcam;
    private Transform player;
    private Vector3 storage = new Vector3(0, 0, 0);


    void Start()
    {
        player = this.transform;
        pcam = player.GetChild(0);
    }

    void Update()
    {
        storage.x -= Input.GetAxis("Mouse Y") * lookSensitivity.y;
        storage.y += Input.GetAxis("Mouse X") * lookSensitivity.x;

        storage.x = Mathf.Clamp(storage.x, -70, 70);

        transform.position += (transform.forward * Input.GetAxis("Vertical")) * .25F;
        transform.position += (transform.right * Input.GetAxis("Horizontal")) * .25F;

        player.rotation = Quaternion.Euler(0, storage.y, 0);
        pcam.localRotation = Quaternion.Euler(storage.x, 0, 0);
    }
}