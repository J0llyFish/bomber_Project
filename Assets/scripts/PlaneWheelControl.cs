using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneWheelControl : MonoBehaviour
{
    public SphereCollider[] wheels;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(switch_cooling > 0f){
            switch_cooling -= Time.deltaTime;
        }else{
            switchLanding();
        }
    }

    float switch_cooling = 0f;
    void switchLanding(){
        if(Input.GetKeyDown(GameController.gameController.keyMap.switch_landing)){
            if(GetComponent<Animator>().GetBool("is_landing")){
                if(transform.position.y > 10){
                    GetComponent<Animator>().SetBool("is_landing",!GetComponent<Animator>().GetBool("is_landing"));
                    for(int i=0;i < wheels.Length;i++){
                        wheels[i].isTrigger = !wheels[i].isTrigger;
                    }
                    switch_cooling = 7f;
                }
            }else{
                GetComponent<Animator>().SetBool("is_landing",!GetComponent<Animator>().GetBool("is_landing"));
                for(int i=0;i < wheels.Length;i++){
                    wheels[i].isTrigger = !wheels[i].isTrigger;
                }
                switch_cooling = 7f;
            }
        }
    }
}
