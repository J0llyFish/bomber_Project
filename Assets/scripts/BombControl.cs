using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombControl : MonoBehaviour
{
    public Rigidbody rigid;
    public float f;
    public GameObject fx;
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
            transform.rotation = Quaternion.LookRotation(rigid.velocity);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "target"){
            Debug.Log("Hit!");
            play_sfx(this.gameObject);
            Destroy(this.gameObject);
        }
        if(collision.gameObject.tag == "friendly"){
            Debug.Log("you betrayed motherland!");
            play_sfx(this.gameObject);
            Destroy(this.gameObject);
        }
        if(collision.gameObject.tag == "terrain"){
            play_sfx(this.gameObject);
            Destroy(this.gameObject);
        }
        
    }
    void play_sfx(GameObject self){
        GameObject ngo = Instantiate(fx);
        ngo.transform.position = self.transform.position;
    }
    
}
