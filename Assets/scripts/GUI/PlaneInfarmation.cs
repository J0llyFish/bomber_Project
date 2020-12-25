using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneInfarmation : MonoBehaviour
{
    public InGameUIControl inGameUIControl;
    public TMPro.TMP_Text speed;
    public TMPro.TMP_Text y_axis_velocity;
    public TMPro.TMP_Text y_axis_distance;
    public TMPro.TMP_Text fuel;
    void Start()
    {
        if(inGameUIControl == null){
            inGameUIControl = FindObjectOfType<InGameUIControl>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        speed.text = (inGameUIControl.plane.plane_rigid.velocity.magnitude / 0.44704f).ToString("0.00") + " mph";
        y_axis_velocity.text = (inGameUIControl.plane.plane_rigid.velocity.y/ 0.44704f).ToString("0.00") + " mph";
        y_axis_distance.text = ((inGameUIControl.plane.transform.position.y - inGameUIControl.plane.reference_height)/0.3048f).ToString("0") + " ft";
        fuel.text = inGameUIControl.plane.fuel.ToString("0.00000") + " l";
    }
}
