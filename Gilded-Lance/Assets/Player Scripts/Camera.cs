using UnityEngine;
using System.Collections;
using System;

public class Camera : MonoBehaviour
{
    public Vector2 lookSensitivity = new Vector2(5, 5);

    private bool useController;
    private Transform pcam;
    private Transform player;
    private Vector2 storage;
    private float conductor;


    void Start() {
        player = this.transform;
        pcam = player.GetChild(0);
    }

    void Update() {
        storage = new Vector2(Mathf.Clamp(storage.x - ((useController) ? Input.GetAxis("Axis Y") : Input.GetAxis("Mouse Y")) * lookSensitivity.y * conductor, -65, 75) // upper limit bounded by min value, lower limit bounded by max value
                                         , storage.y + ((useController) ? Input.GetAxis("Axis X") : Input.GetAxis("Mouse X")) * lookSensitivity.x * conductor);

        player.rotation = Quaternion.Euler(0, storage.y, 0);
        pcam.localRotation = Quaternion.Euler(storage.x, 0, 0);
    }

    internal Ray ScreenPointToRay(Vector3 mousePosition)
    {
        throw new NotImplementedException();
    }
}