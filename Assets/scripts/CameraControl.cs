using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float cameraSensitivity;
    Vector2 mouseLook;
    public float movingSpeed;
    public GameObject rotator;
    public float zoom_in = 10f;
    public int type = 1;
    
    void Start()
    {
        
    }
    void Update()
    {
        if(type == 1){
            camera_control_type_1();
        }
        if(type == 2){
            camera_control_type_2();
        }
    }
    void camera_control_type_1(){
        Vector2 mousePosition = new Vector2(Input.GetAxisRaw("Mouse X") * cameraSensitivity, Input.GetAxisRaw("Mouse Y") * cameraSensitivity);
        mouseLook = new Vector2( mouseLook.x + mousePosition.x, Mathf.Clamp(mouseLook.y + mousePosition.y, -180, 180));
        Vector3 targetRot = new Vector3(-mouseLook.y,mouseLook.x);
        transform.eulerAngles = targetRot;
        transform.position = rotator.transform.position - transform.forward * zoom_in;
    }

    void camera_control_type_2(){
        Vector2 mousePosition = new Vector2(Input.GetAxisRaw("Mouse X") * cameraSensitivity, Input.GetAxisRaw("Mouse Y") * cameraSensitivity);
        mouseLook = new Vector2( mouseLook.x + mousePosition.x, Mathf.Clamp(mouseLook.y + mousePosition.y, -180, 180));
        transform.eulerAngles = new Vector3(-mouseLook.y,mouseLook.x);
        //Vector3 targetRot = new Vector3(-mouseLook.y,mouseLook.x);
        //transform.eulerAngles = targetRot;
        //transform.position = rotator.transform.position - transform.forward * zoom_in;
    }

    
}
