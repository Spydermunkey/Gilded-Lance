using UnityEngine;
using System.Collections;

public class GeneralBehaviour : MonoBehaviour
{
    public Vector2 lookSensitivity = new Vector2(5, 5);

    private Transform pcam;
    private Transform player;
    private Vector2 storage;

    //new vars
    private Rigidbody rigid;
    private float jumpTimer;
    private float conductor;
    public Vector3 jump = new Vector3(0, 5, 0); 

    void Start()
    {
        player = this.transform;
        pcam = player.GetChild(0);
        rigid = player.GetComponent<Rigidbody>();
    }

    void Update(){
        conductor = Time.smoothDeltaTime;
        jumpTimer -= conductor;
        storage = new Vector2(Mathf.Clamp(storage.x - Input.GetAxis("Mouse Y") * lookSensitivity.y * conductor, -95, 85) // upper limit bounded by min value, lower limit bounded by max value
                                        , storage.y + Input.GetAxis("Mouse X") * lookSensitivity.x * conductor);

        player.position += (player.forward * Input.GetAxis("Vertical")) / 4;
        player.position += (player.right * Input.GetAxis("Horizontal")) / 4;

        if (Input.GetButtonDown("Jump") && jumpTimer < 0 ) { 
        
            rigid.velocity += jump;
            jumpTimer = 2;
        }
        player.rotation = Quaternion.Euler(0, storage.y, 0);
        pcam.localRotation = Quaternion.Euler(storage.x, 0, 0);
    }
}