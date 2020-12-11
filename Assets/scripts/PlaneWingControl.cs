using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneWingControl : MonoBehaviour
{
    public PlaneControl plane;
    [Tooltip("empirical number for making plane rotate,just for jun :)")]
    public float rotate_coeff = 750000f;
    [Tooltip("this factor affect duration of a wing reaching it's maximum angle")]
    public float time_factor = 40f;
    [Tooltip("maximum angle that a wing can do in degree")]
    public float max_rotate_angle = 30f;
    public GameObject main_left_wings,main_right_wings,vertical_back_wings,horizontal_back_wings;
    //public GameObject centor_left_wings,centor_right_wings;
    public float main_angle;//main_right_angle;
    public float vertical_back_angle,horizontal_back_angle;
    // Update is called once per frame
    void Update()
    {
        main_angle=0;//main_right_angle=0;
        vertical_back_angle=0;horizontal_back_angle=0;
        if(Input.GetKey(GameController.gameController.keyMap.plane_rotate_ccw)){
            plane.plane_rigid.AddRelativeTorque(Vector3.forward* rotate_coeff * Time.deltaTime);
            main_angle -= max_rotate_angle;
        }
        if(Input.GetKey(GameController.gameController.keyMap.plane_rotate_cw)){
            plane.plane_rigid.AddRelativeTorque(-Vector3.forward* rotate_coeff * Time.deltaTime);
            main_angle += max_rotate_angle;
        }
        if(Input.GetKey(GameController.gameController.keyMap.plane_onward)){
            plane.plane_rigid.AddRelativeTorque(Vector3.right* rotate_coeff * Time.deltaTime);
            horizontal_back_angle +=max_rotate_angle;
        }
        if(Input.GetKey(GameController.gameController.keyMap.plane_downward)){
            plane.plane_rigid.AddRelativeTorque(Vector3.left* rotate_coeff * Time.deltaTime);
            horizontal_back_angle -=max_rotate_angle;
        }
        if(Input.GetKey(GameController.gameController.keyMap.plane_rotate_shift_left)){
            plane.plane_rigid.AddRelativeTorque(Vector3.up* rotate_coeff * Time.deltaTime);
            vertical_back_angle -= max_rotate_angle;
        }
        if(Input.GetKey(GameController.gameController.keyMap.plane_rotate_shift_right)){
            plane.plane_rigid.AddRelativeTorque(-Vector3.up* rotate_coeff * Time.deltaTime);
            vertical_back_angle += max_rotate_angle;
        }
        vertical_back_control();
        horizontal_back_control();
        main_wing_control();
    }
    float main_angle_control=0,vertical_back_angle_control=0,horizontal_back_angle_control=0;
    
    void vertical_back_control(){
        //Debug.Log(vertical_back_wings.transform.localRotation.eulerAngles.y);
        vertical_back_angle_control += (vertical_back_angle-vertical_back_angle_control)*Time.deltaTime*time_factor;
        vertical_back_wings.transform.localRotation = Quaternion.Euler(0f,vertical_back_angle_control,0f);
    }
    void horizontal_back_control(){
        //Debug.Log(vertical_back_wings.transform.localRotation.eulerAngles.y);
        horizontal_back_angle_control += (horizontal_back_angle-horizontal_back_angle_control)*Time.deltaTime*time_factor;
        horizontal_back_wings.transform.localRotation = Quaternion.Euler(horizontal_back_angle_control,0f,0f);
    }
    void main_wing_control(){
        //Debug.Log(vertical_back_wings.transform.localRotation.eulerAngles.y);
        main_angle_control += (main_angle-main_angle_control)*Time.deltaTime*time_factor;
        main_left_wings.transform.localRotation = Quaternion.Euler(-main_angle_control,main_left_wings.transform.localRotation.eulerAngles.y,main_left_wings.transform.localRotation.eulerAngles.z);
        main_right_wings.transform.localRotation = Quaternion.Euler(main_angle_control,main_right_wings.transform.localRotation.eulerAngles.y,main_right_wings.transform.localRotation.eulerAngles.z);
    }
}
