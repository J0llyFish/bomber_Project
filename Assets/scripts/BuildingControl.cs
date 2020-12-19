using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingControl : MonoBehaviour
{
    public float hp = 100f;
    public GameObject flare_position;
    public GameObject flare;
    private float death_timer = 5;
    private float death_speed = 0;
    public GameObject[] AAs;
    void Start()
    {
        
    }

    void Update()
    {
        if(hp<=0){
            destory();
        }
    }

    void destory(){
        transform.position -= new Vector3(0,death_speed*Time.deltaTime,0);
        death_speed += 9.81f*Time.deltaTime;
        if(death_timer>0){
            death_timer -= Time.deltaTime;
        }else{
            destory_AA();
            Destroy(this.gameObject);
        }
    }
    void destory_AA(){
        for(int i=0;i<AAs.Length;i++){
            Destroy(AAs[i]);
        }
    }
}
