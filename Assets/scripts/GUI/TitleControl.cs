using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TitleControl : MonoBehaviour
{
    
    public AudioSource bgm_title;
    public GameObject choose_mission;
    public InputField input_name;
    void Start()
    {
        first_settong();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void first_settong(){
        bgm_title.time = 2.45f;
    }

    public void open_choose_mission(){
        choose_mission.SetActive(true);
    }
    public void close_choose_mission(){
        choose_mission.SetActive(false);
    }
    public void quit_game(){
        Application.Quit();
    }
    public void start_mission_one(){
        SceneManager.LoadScene(1);
    }
    public void set_name(){
        
    }
}
