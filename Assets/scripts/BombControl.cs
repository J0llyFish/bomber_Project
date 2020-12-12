using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombControl : MonoBehaviour
{
    public Rigidbody rigid;
    public float f;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        //rigid.velocity = new Vector3(31f,5.2f,45f);
    }

    // Update is called once per frame
    void Update()
    {
        if(rigid.velocity.magnitude > 0.0001){
            //transform.rotation=Quaternion.Euler(rigid.velocity);
            //transform.rotation = Quaternion.LookRotation(transform.position,transform.position+rigid.velocity);
            //transform.rotation = Quaternion.LookRotation(transform.position,transform.position-rigid.velocity);
            transform.rotation = Quaternion.LookRotation(rigid.velocity);
        }   // .LookRotation
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "target"){
            Debug.Log("Hit!");
            Destroy(this.gameObject);
        }
        
    }
    
}
