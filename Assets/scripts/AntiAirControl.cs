using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiAirControl : MonoBehaviour
{
    //public List<GameObject> enemys;
    public float hp = 20f;
    public GameObject bullets;
    public GameObject target;
    public Vector3 distance;
    public GameObject rotator;
    public GameObject aimPosition;
    public bool isLoad = true;
    public float rotateSpeed_deg_per_sec = 45f;
    public float directionalTolerance = 10;
    public float cooling = 0.6f;
    public float phi,theta;
    public float curr_phi=0,curr_theta=0;
    //private bool redirecting = false;
    
    void Start()
    {
        //enemys = new List<GameObject>();
        curr_phi=90;curr_theta=0;

    }

    void Update()
    {
        if(target != null){
            calcDistance();
            changeDesiredDirection();
        }
    }

    void calcDistance(){
        distance = target.transform.position - transform.position;
        phi = Mathf.Acos(distance.y / distance.magnitude)*180f/Mathf.PI;
        theta = Mathf.Atan(distance.z/distance.x)*180f/Mathf.PI;
        if(distance.x>0f){
            theta = -theta+180;
        }else{
            theta = -theta;
        }
    }
    void changeDesiredDirection(){
        if(phi-curr_phi >= 0){
            curr_phi += Time.deltaTime *rotateSpeed_deg_per_sec;
        }else{
            curr_phi -= Time.deltaTime *rotateSpeed_deg_per_sec;
        }
        if(curr_theta+90 > 180 ){
            if(curr_theta - theta > 0){
                curr_theta -= Time.deltaTime *rotateSpeed_deg_per_sec;
            }else{
                curr_theta += Time.deltaTime *rotateSpeed_deg_per_sec;
            }
        }else{
            //Debug.Log("curr_theta+90 < 180");
            if(curr_theta - theta > 0){
                curr_theta -= Time.deltaTime *rotateSpeed_deg_per_sec;
            }else{
                curr_theta += Time.deltaTime *rotateSpeed_deg_per_sec;
            }
        }
        if(curr_theta+90 > 360){
            curr_theta -= 360f;
        }
        if(curr_theta+90 < -360){
            curr_theta += 360;
        }
        curr_phi = Mathf.Clamp(curr_phi,0,90f);
        reSetDirection();
        fireCheck();
    }
    void reSetDirection(){
        rotator.transform.localRotation = Quaternion.Euler(rotator.transform.localRotation.eulerAngles.x,rotator.transform.localRotation.eulerAngles.y,curr_phi-90f);
        rotator.transform.localRotation = Quaternion.Euler(rotator.transform.localRotation.eulerAngles.x,curr_theta,rotator.transform.localRotation.eulerAngles.z);
    }

    bool checkDirectionDeviation(){
        if(Mathf.Abs(curr_theta - theta)+Mathf.Abs(curr_phi - phi) <= directionalTolerance){
            return true;//good
        }
        return false; //bad
    }
    float cooling_timer;
    void fireCheck(){
        if(checkDirectionDeviation()){
            if(isLoad){
                GameObject n_bullet = Instantiate(bullets);
                n_bullet.transform.position = aimPosition.transform.position;
                n_bullet.transform.rotation = Quaternion.Euler(aimPosition.transform.position - transform.position);
                n_bullet.GetComponent<Rigidbody>().velocity = 160 * (aimPosition.transform.position - transform.position);
                n_bullet.GetComponent<AABulletControl>().AntiAirCannon = this.gameObject;
                isLoad = false;cooling_timer = cooling;
                GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
            }else{
                if(cooling_timer<=0){
                    isLoad = true;
                }else{
                    cooling_timer -= Time.deltaTime;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            target = other.gameObject;
        }
        // if(other.gameObject.tag == "Player"){
        //     bool check = false;
        //     for(int i=0;i < enemys.Count;i++){
        //         if(enemys[i] == other.gameObject){
        //             check = true;
        //         }
        //     }
        //     if(!check){
        //         enemys.Add(other.gameObject);
        //     }
        // }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            target = null;
        }
        // if(other.gameObject.tag == "Player"){
        //     for(int i=0;i < enemys.Count;i++){
        //         if(enemys[i] == other.gameObject){
        //             enemys[i] = null;
        //         }
        //     }
        // }
    }
}
