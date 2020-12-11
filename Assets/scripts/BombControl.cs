using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombControl : MonoBehaviour
{
    public Rigidbody rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        //rigid.AddForce(2000,2000,2000);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(rigid.velocity.magnitude > 0.0001){
            //transform.rotation=Quaternion.Euler(rigid.velocity);
            transform.rotation = Quaternion.LookRotation(transform.position,transform.position+rigid.velocity);
        }
    }

    
}
