using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberControl : MonoBehaviour
{
    public PlaneControl plane;
    public GameObject bomber_camera;
    //public float y_axis_origin_point = 0;
    // Start is called before the first frame update
    public GameObject drop_position , bomb;
    public float terrain_height = 0;
    public LayerMask layer_mask_for_terrain;
    public AudioSource bomb_audio;
    void Start()
    {
        
    }

    void Update()
    {
        get_height_terrain();
        set_cam();
        if(Input.GetKeyDown(GameController.gameController.keyMap.drop_bomb)){
            drop_bomb();
        }
    }
    Ray ray;RaycastHit hitInfo;
    void get_height_terrain(){
        ray = new Ray (transform.position, new Vector3(0,-1,0));
		if (Physics.Raycast (ray, out hitInfo, 10000f,layer_mask_for_terrain,QueryTriggerInteraction.Collide)) {
			//Debug.DrawLine (ray.origin, hitInfo.point, Color.red);
            terrain_height = hitInfo.point.y;
		} else {
			//Debug.DrawLine (ray.origin, ray.origin + ray.direction * 100, Color.green);
            terrain_height = 10000f;
		}
    }
    float planar_vecocity,s,s_2,t,planar_s,tan,t1,t2,deg;
    void set_cam(){
        planar_vecocity=Mathf.Sqrt(Mathf.Pow(plane.plane_rigid.velocity.x,2)+Mathf.Pow(plane.plane_rigid.velocity.z,2));
        s = Mathf.Clamp(plane.transform.position.y - terrain_height,0.0001f,999999f);
        //t = Mathf.Sqrt(0.66667f*s/9.81f);
        if(plane.plane_rigid.velocity.y>=0){
            //Debug.Log("type_1");
            t1 = plane.plane_rigid.velocity.y/9.81f;
            s_2 = (float)(plane.plane_rigid.velocity.y*t1+0.5*9.81*t1*t1);
            t2 = Mathf.Sqrt(2*(s_2+s)/9.81f);
        }else{
            t1 = -plane.plane_rigid.velocity.y/9.81f;
            s_2 = (float)(-plane.plane_rigid.velocity.y*t1+0.5*9.81*t1*t1);
            t2 = Mathf.Sqrt(2*(s_2+s)/9.81f) - t1;
            t1=0;
        }
        //Debug.Log("t1="+t1+"t2="+t2);
        planar_s = planar_vecocity * (t1+t2) ;
        tan = s/planar_s+0.000001f;
        if(plane.transform.rotation.eulerAngles.x >180){
            deg = plane.transform.rotation.eulerAngles.x - 360f;
        }else{
            deg = plane.transform.rotation.eulerAngles.x;
        }
        bomber_camera.transform.localRotation = Quaternion.Euler(Mathf.Atan(tan)*180f/Mathf.PI-deg,0,0);
        //Debug.Log("tan"+tan);
    }

    
    void drop_bomb(){
        GameObject new_bomb = Instantiate(bomb);
        new_bomb.transform.position = drop_position.transform.position;
        new_bomb.transform.rotation = plane.transform.localRotation;
        //Debug.Log(plane.plane_rigid.velocity);
        new_bomb.GetComponent<Rigidbody>().velocity = new Vector3(plane.plane_rigid.velocity.x,plane.plane_rigid.velocity.y,plane.plane_rigid.velocity.z);
        //Debug.Log(new_bomb.GetComponent<Rigidbody>().velocity);
        bomb_audio.PlayOneShot(bomb_audio.clip);
    }
}
