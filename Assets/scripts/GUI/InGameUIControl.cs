using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InGameUIControl : MonoBehaviour
{
    public PlaneControl plane;
    public Text plane_vector;
    public Text degree;
    public RawImage campass;public Image map;
    [Tooltip("deviation of uvRect.x of north and second north")]
    public float degree_360_to_uvRect;
    float uvRect_x_north;
    public bool ended;
    public GameObject end_game_GUI;
    public TMPro.TMP_Text end_title;public TMPro.TMP_Text end_exit_sttring;
    public AudioClip lost_music,win_music;
    public Image bomb_position_aim;
    public int bomb_camera_index =2;
    public GameObject plane_information_gui;
    void Start()
    {
        uvRect_x_north = campass.uvRect.x;
    }

    void Update()
    {
        setDegree();
        Campass();
        toggleMap();
        if(!ended){
            if(GameController.gameController.win){
                endGame(true);
                ended = !ended;
            }else if(GameController.gameController.lose){
                endGame(false);
                ended = !ended;
            }
        }
        return_to_menu();
        close_end_game_GUI();toggle_information_GUI();
        bomb_position_gui_control();
    }

    public float plane_degree;
    void setDegree(){
        if(plane != null){
            plane_vector.text = plane.plane_vector.ToString();
            if(plane.plane_vector.x>=0){
                plane_degree = 90-Mathf.Atan(plane.plane_vector.z / plane.plane_vector.x ) *180f / 3.14159f;;
                degree.text = plane_degree.ToString();
            }else{
                plane_degree = 270-Mathf.Atan(plane.plane_vector.z / plane.plane_vector.x ) *180f / 3.14159f;;
                degree.text = plane_degree.ToString();
            }
            // plane_degree = Mathf.Atan(plane.plane_vector.z / plane.plane_vector.x );
            // plane_degree = plane_degree *180f / 3.14159f;
            // degree.text = plane_degree.ToString();
        }
    }
    
    void Campass(){
        if(plane_degree <= 180){
            campass.uvRect = new Rect(uvRect_x_north+plane_degree*degree_360_to_uvRect/360f,campass.uvRect.y,campass.uvRect.width,campass.uvRect.height);
        }else{
            campass.uvRect = new Rect(uvRect_x_north+plane_degree*degree_360_to_uvRect/360f - degree_360_to_uvRect,campass.uvRect.y,campass.uvRect.width,campass.uvRect.height);
        }
    }

    void toggleMap(){
        if(Input.GetKeyDown(GameController.gameController.keyMap.toggle_map)){
            map.gameObject.SetActive(!map.gameObject.activeSelf);
        }
    }
    public void endGame(bool end_condition){
        end_game_GUI.SetActive(true);
        if(end_condition){
            end_title.text = "you succefully completed the mission!";
            end_exit_sttring.text = "press " + GameController.gameController.keyMap.return_to_menu
            + " to return to menu\n"+"press "+GameController.gameController.keyMap.control_key+" to continue";
            GameController.gameController.playerData.win_time ++;
            GameController.gameController.playerData.betraying_motherland = false;
            GetComponent<AudioSource>().PlayOneShot(win_music);
        }else{
            end_title.text = "you betrayed motherland!";
            end_exit_sttring.text = "press " + GameController.gameController.keyMap.return_to_menu
            + " to be send to GULAG\n"+"press "+GameController.gameController.keyMap.control_key+" to continue";
            GameController.gameController.playerData.lose_time ++;
            GameController.gameController.playerData.betraying_motherland = true;
            GetComponent<AudioSource>().PlayOneShot(lost_music);
        }
    }

    public void close_end_game_GUI(){
        if(Input.GetKeyDown(GameController.gameController.keyMap.control_key)){
            end_game_GUI.SetActive(false);
        }
    }

    public void return_to_menu(){
        if(Input.GetKeyDown(GameController.gameController.keyMap.return_to_menu)){
            if(GameController.gameController.house_destoryed >= 20){
                GameController.gameController.playerData.medaled = 1;
            }
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    public void bomb_position_gui_control(){
        if(GameController.gameController.camera_index == bomb_camera_index){
            if(!bomb_position_aim.gameObject.activeSelf){
                bomb_position_aim.gameObject.SetActive(true);
            }
        }else{
            if(bomb_position_aim.gameObject.activeSelf){
                bomb_position_aim.gameObject.SetActive(false);
            }
        }
    }
    void toggle_information_GUI(){
        if(Input.GetKeyDown(GameController.gameController.keyMap.control_key)){
            plane_information_gui.SetActive(!plane_information_gui.activeSelf);
        }
    }
}
