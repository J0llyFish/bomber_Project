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
            }else if(GameController.gameController.lose){
                endGame(false);
            }
        }
        return_to_menu();
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
            + " to return to menu";
        }else{
            end_title.text = "you betrayed motherland!";
            end_exit_sttring.text = "press " + GameController.gameController.keyMap.return_to_menu
            + " to be send to GULAG";
        }
    }

    public void return_to_menu(){
        if(Input.GetKeyDown(GameController.gameController.keyMap.return_to_menu)){
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
