using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABulletControl : MonoBehaviour
{
    
    public GameObject AntiAirCannon;
    public float bullet_max_distance = 1500f;
    float min_tm = 0;
    public GameObject no_hit_fx;
    public float start_count_timer = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkDistance();
    }

    void checkDistance(){
        min_tm += Time.deltaTime;
        if(min_tm>4f){
            if(AntiAirCannon != null){
                if((transform.position - AntiAirCannon.transform.position).magnitude > bullet_max_distance){
                    if(no_hit_fx != null){
                        GameObject ng = Instantiate(no_hit_fx);
                        ng.transform.position = transform.position;
                    }
                    Destroy(this.gameObject);
                }
            }else{
                Destroy(this.gameObject);
            }
            
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("plane get hit!");
    }
}
