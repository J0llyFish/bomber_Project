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
    public TMPro.TMP_Text pressure;
    public TMPro.TMP_Text power_exported,radio_frequency,building_destroyed,times_get_hit;
    public TMPro.TMP_Text hint;
    void Start()
    {
        if(inGameUIControl == null){
            inGameUIControl = FindObjectOfType<InGameUIControl>();
        }
        hint.text = "press ["+GameController.gameController.keyMap.control_key+"] to toggle dashbroad";
    }

    // Update is called once per frame
    void Update()
    {
        speed.text = (inGameUIControl.plane.plane_rigid.velocity.magnitude / 0.44704f).ToString("0.00") + " mph";
        y_axis_velocity.text = (inGameUIControl.plane.plane_rigid.velocity.y* 3.2808f).ToString("0.00") + " ft/s";
        y_axis_distance.text = ((inGameUIControl.plane.transform.position.y - inGameUIControl.plane.reference_height)/0.3048f).ToString("0") + " ft";
        fuel.text = (inGameUIControl.plane.fuel / 3.785).ToString("0.00") + " gal";
        pressure.text = (inGameUIControl.plane.pressure /101325f).ToString("0.0000") + " atm";
        power_exported.text = (inGameUIControl.plane.get_engine_exported_power()/1000f).ToString("0.0")+" kW";
        radio_frequency.text = inGameUIControl.radio_control.frequency_curr.ToString("0.0");
        building_destroyed.text = GameController.gameController.house_destoryed.ToString();
        times_get_hit.text = inGameUIControl.plane.times_get_hit.ToString();
    }
}
