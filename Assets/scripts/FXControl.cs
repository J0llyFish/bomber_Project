using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXControl : MonoBehaviour
{
    public float timer;
    void Start()
    {
        
    }    
    void Update()
    {
        if(timer <= 0){
            Destroy(this.gameObject);
        }else{
            timer -= Time.deltaTime;
        }
    }
}
