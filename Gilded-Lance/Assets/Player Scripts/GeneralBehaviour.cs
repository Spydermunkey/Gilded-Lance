using UnityEngine;
using System.Collections;

public class GeneralBehaviour : MonoBehaviour
{
    public Vector2 lookSensitivity = new Vector2(5, 5);
    public bool useController = true;

    private Transform pcam;
    private Transform player;
    private Vector2 storage;
    public float speed = 4;

    //new vars
    private Rigidbody rigid;
    private float jumpTimer;
    private float conductor;
    public Vector3 jump = new Vector3(0, 5, 0); 

    void Start() {
        player = this.transform;
        pcam = player.GetChild(0);
        rigid = player.GetComponent<Rigidbody>();
    }

    void Update() {
        conductor = Time.smoothDeltaTime;
        jumpTimer -= conductor;

        if (Input.GetButtonDown("Run"))
            speed = 3;
        else if (Input.GetButtonUp("Run"))
            speed = 4;

        storage = new Vector2(Mathf.Clamp(storage.x - ((useController) ? Input.GetAxis("Axis Y") : Input.GetAxis("Mouse Y")) * lookSensitivity.y * conductor, -65, 75) // upper limit bounded by min value, lower limit bounded by max value
                                        , storage.y + ((useController) ? Input.GetAxis("Axis X") : Input.GetAxis("Mouse X")) * lookSensitivity.x * conductor);

        player.position += (player.forward * Input.GetAxis("Vertical")) / speed;
        player.position += (player.right * Input.GetAxis("Horizontal")) / speed;

        if (Input.GetButtonDown("Jump") && jumpTimer < 0 ) { 
        
            rigid.velocity += jump;
            jumpTimer = 2;
        }

        

        player.rotation = Quaternion.Euler(0, storage.y, 0);
        pcam.localRotation = Quaternion.Euler(storage.x, 0, 0);
    }
}