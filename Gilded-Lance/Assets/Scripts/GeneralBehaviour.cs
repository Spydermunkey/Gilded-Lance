using UnityEngine;
using System.Collections;

public class GeneralBehaviour : MonoBehaviour
{
    public Vector2 lookSensitivity = new Vector2(5, 5);

    private Transform pcam;
    private Transform player;
    private Vector2 storage;
    // rip speed :(

    //rip Use controller :(
    // new vars 
    private Transform cursorCube;
    private Vector3 posKeeper;
    private Vector3 otherRefVec;
    private float t = 0.2f;
    //use debug inspector to view
    private bool grounded;
    private Vector3 refVec;
    private Transform playerBlock;
    private float coolTime;
    private Transform trans;

    private Rigidbody rigid;
    //rip jumptimer :(
    private float conductor;
    public Vector3 jump = new Vector3(0, 5, 0); 

    void Start() {
        player = this.transform;
        pcam = player.GetChild(0);
        rigid = player.GetComponent<Rigidbody>();
        cursorCube = player.GetChild(1);
        playerBlock = player.GetChild(2);
        player.DetachChildren();
        pcam.parent = player;
        trans = playerBlock.GetChild(0);
        
    }

    void Update() {
        conductor = Time.smoothDeltaTime;
        if (coolTime > 0)
            coolTime -= conductor;
        RaycastHit hit;
        if (Physics.Raycast(pcam.position, pcam.forward, out hit)){

            cursorCube.position = Vector3.SmoothDamp(cursorCube.position, new Vector3(hit.point.x, hit.point.y + 1, hit.point.z), ref refVec, 0.1f);
        }

        if (Input.GetButtonDown("Fire1") && coolTime <= 0){
            posKeeper = cursorCube.position;
            rigid.useGravity = false;
        }
        if (Input.GetButtonDown("Fire2")) { 
        
            playerBlock.position = cursorCube.position;
            playerBlock.LookAt(player);
        }
        if (Input.GetButtonDown("Fire3"))
            trans.GetComponent<Camera>().enabled = !trans.GetComponent<Camera>().enabled;

        if (Input.GetAxis("Mouse ScrollWheel") != 0) {

           trans.gameObject.SetActive(true);
            Camera cam = pcam.GetComponent<Camera>();
            cam.fieldOfView = 30f;

        }

        if (posKeeper != Vector3.zero) {
            coolTime = 1.98f;
            player.position = Vector3.SmoothDamp(player.position, posKeeper, ref otherRefVec, 0.1f);
            t -= conductor;
            if (t < 0) {

                posKeeper = Vector3.zero;
                rigid.useGravity = true;
                rigid.velocity = posKeeper;
                t = 0.2f;
            }

        }
        Debug.DrawRay(pcam.position, -pcam.forward, Color.green);
        storage = new Vector2(Mathf.Clamp(storage.x - Input.GetAxis("Mouse Y") * lookSensitivity.y * conductor, -95, 85)
                                        , storage.y + Input.GetAxis("Mouse X") * lookSensitivity.x * conductor);

        player.rotation = Quaternion.Euler(0, storage.y, 0);
        pcam.localRotation = Quaternion.Euler(storage.x, 0, 0);
    }

}