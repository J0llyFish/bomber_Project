using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberControl : MonoBehaviour
{
    public PlaneControl plane;
    public float y_axis_origin_point = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    float planar_vecocity,s,t,planar_s,tan,t1,t2,deg;
    void Update()
    {
        planar_vecocity=Mathf.Sqrt(Mathf.Pow(plane.plane_rigid.velocity.x,2)+Mathf.Pow(plane.plane_rigid.velocity.y,2));
        s = Mathf.Clamp(plane.transform.position.y - y_axis_origin_point,0.0001f,999999f);
        //t = Mathf.Sqrt(0.66667f*s/9.81f);
        t1 = plane.plane_rigid.velocity.y/9.81f;
        t2 = Mathf.Sqrt(2*s/9.81f);
        planar_s = planar_vecocity * (t1+t2) ;
        tan = s/planar_s;
        if(plane.transform.rotation.eulerAngles.x >180){
            deg = plane.transform.rotation.eulerAngles.x - 360f;
        }else{
            deg = plane.transform.rotation.eulerAngles.x;
        }
        transform.localRotation = Quaternion.Euler(Mathf.Atan(tan)*180f/Mathf.PI-deg,0,0);
        //Debug.Log("tan"+tan);
    }
}
