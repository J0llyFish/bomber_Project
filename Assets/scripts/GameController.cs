using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController gameController;
    public KeyMap keyMap;

    public GameObject[] cameras;
    public int camera_index = 0;
    void Awake(){
        if(gameController == null){
            gameController = this;
        }else{
            Destroy(this.gameObject);
        }
        //startSetCam();
    }
    void startSetCam(){
        for(int i=0;i < cameras.Length;i++){
            cameras[i].SetActive(false);
        }
        cameras[0].SetActive(true);
        camera_index = 0;
    }

    void Update(){
        changeCam();
        //pause_hand();
    }

    void changeCam(){
        if(Input.GetKeyDown(GameController.gameController.keyMap.cgange_camera)){
            if(camera_index == cameras.Length-1){
                for(int i=0;i < cameras.Length;i++){
                    cameras[i].SetActive(false);
                }
                cameras[0].SetActive(true);
                camera_index = 0;
            }else{
                for(int i=0;i < cameras.Length;i++){
                    cameras[i].SetActive(false);
                }
                cameras[camera_index+1].SetActive(true);
                camera_index++;
            }
        }
    }
    //public GameObject plane_debug;
    bool pause = false;
    void pause_hand(){
        if(Input.GetKeyDown(KeyCode.F1)){
            if(!pause){
                Time.timeScale = 0;
                pause = true;
                // if(plane_debug != null){
                //     Rigidbody rigid = plane_debug.GetComponent<PlaneControl>().plane_rigid;
                //     Debug.Log("planar="+Mathf.Sqrt(Mathf.Pow(rigid.velocity.x,2)+Mathf.Pow(rigid.velocity.z,2)));
                //     Debug.Log("vertical="+rigid.velocity.y);
                // }
            }else{
                Time.timeScale = 1;
                pause = false;
            }
        }
    }
}
