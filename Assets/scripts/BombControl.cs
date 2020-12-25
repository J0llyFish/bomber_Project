using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombControl : MonoBehaviour
{
    public Rigidbody rigid;
    public GameObject fx;
    public float damage;
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
        if(transform.position.y < -1f){
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "target"){
            if(collision.gameObject.GetComponent<BuildingControl>() != null){
                BuildingControl building = collision.gameObject.GetComponent<BuildingControl>();
                building.hp -= damage;
                if(building.hp<50){
                    if(building.flare_position != null){
                        GameObject go = Instantiate(building.flare);
                        go.transform.parent = building.transform;
                        go.transform.position = building.flare_position.transform.position;
                    }
                }
            }
            Debug.Log("Hit!");
            play_sfx(this.gameObject);
            Destroy(this.gameObject);
        }
        if(collision.gameObject.tag == "friendly"){
            Debug.Log("you betrayed motherland!");
            //
            GameController.gameController.lose = true;
            //
            if(collision.gameObject.GetComponent<BuildingControl>() != null){
                for(int i=0;i < collision.gameObject.GetComponent<BuildingControl>().AAs.Length;i++){
                    if(collision.gameObject.GetComponent<BuildingControl>().AAs[i].GetComponent<AntiAirControl>() != null){
                        collision.gameObject.GetComponent<BuildingControl>().AAs[i].GetComponent<AntiAirControl>().enabled = true;
                    }
                }
            }
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
