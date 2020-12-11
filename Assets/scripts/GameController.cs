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

        for(int i=0;i < cameras.Length;i++){
            cameras[i].SetActive(false);
        }
        cameras[0].SetActive(true);
        camera_index = 0;
    }

    void Update(){
        changeCam();
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
}
