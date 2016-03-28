using UnityEngine;
using System.Collections;

public class GeneralBehaviour : MonoBehaviour
{
    public Vector2 lookSensitivity = new Vector2(5, 5);

    private Vector3 otherRefVec;
    private Vector3 refVec;
    private Vector3 posKeeper;
    private Vector2 storage;
      
    private Transform cursorCube;
    private Transform pcam;
    private Transform player;
    private Transform playerBlock;
    private Transform trans;

    private float conductor;
    private float coolTime;
    private float t = 0.2f;

    private Camera cam;
    private Camera overWatch;

    private Rigidbody rigid;

    void Start() {
        player = this.transform;
        pcam = player.GetChild(0);
        rigid = player.GetComponent<Rigidbody>();
        cursorCube = player.GetChild(1);
        trans = player.GetChild(2).GetChild(0);
        overWatch = player.GetChild(3).GetChild(0).GetComponent<Camera>();
        player.DetachChildren();
        pcam.parent = player;
        overWatch.transform.parent = player;
        cam = trans.GetComponent<Camera>();
        
    }

    void Update() {
        conductor = Time.smoothDeltaTime;
        if (coolTime > 0)
            coolTime -= conductor;

        RaycastHit hit;
        if (Physics.Raycast(pcam.position, pcam.forward, out hit)){
            Vector3 hitPos = hit.point;
            cursorCube.position = Vector3.SmoothDamp(cursorCube.position, new Vector3(hitPos.x, hitPos.y + 1.2f, hitPos.z), ref refVec, 0.1f);
        }
        
        if (Input.GetButtonDown("Fire1") && coolTime <= 0){
            coolTime = 1.98f;
            posKeeper = cursorCube.position;
            rigid.useGravity = false;
        }
        else if (Input.GetButtonDown("Fire2")) { 
        
            trans.position = cursorCube.position;
            trans.LookAt(player);
        }
        else if (Input.GetButtonDown("Fire3")) {
           
           cam.enabled = !cam.enabled;
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") != 0) {
       
            if(cam == trans.GetComponent<Camera>()) {
          
                cam.depth = -1;
                cam.enabled = false;
                cam = overWatch;
                cam.enabled = true;
                cam.depth = 2;
            }
            else if (cam == overWatch)
            {
        
                cam.depth = -1;
                cam.enabled = false;
                cam = trans.GetComponent<Camera>();
                cam.enabled = true;
                cam.depth = 2;
            }

        }
        
        if (posKeeper != Vector3.zero) {

            player.position = Vector3.SmoothDamp(player.position, posKeeper, ref otherRefVec, 0.1f);
            t -= conductor;
            if (t < 0) {

                posKeeper = Vector3.zero;
                rigid.useGravity = true;
                rigid.velocity = posKeeper;
                t = 0.3f;
            }
      
        }
        storage = new Vector2(Mathf.Clamp(storage.x - Input.GetAxis("Mouse Y") * lookSensitivity.y * conductor, -95, 85)
                                        , storage.y + Input.GetAxis("Mouse X") * lookSensitivity.x * conductor);

        player.rotation = Quaternion.Euler(0, storage.y, 0);
        pcam.localRotation = Quaternion.Euler(storage.x, 0, 0);
    }

}