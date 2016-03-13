using UnityEngine;
using System.Collections;


public class Camera : MonoBehaviour
{
    public Vector2 lookSensitivity = new Vector2(5, 5);
	//my nipples have titties that are red bottom with green tops

    private Transform playercam;
    private Transform player;
    private Vector3 storage = new Vector3(0, 0, 0);


    void Start()
    {
        player = this.transform;
        playercam = player.GetChild(0);
    }

    void Update()
    {
        storage.x -= Input.GetAxis("Mouse Y") * lookSensitivity.y;
        storage.y += Input.GetAxis("Mouse X") * lookSensitivity.x;

        storage.x = Mathf.Clamp(storage.x, -70, 70);

        player.rotation = Quaternion.Euler(0, storage.y, 0);
        playercam.localRotation = Quaternion.Euler(storage.x, 0, 0);
    }
}