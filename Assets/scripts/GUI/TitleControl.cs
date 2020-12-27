using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TitleControl : MonoBehaviour
{
    
    public AudioSource bgm_title;
    public GameObject choose_mission;
    public TMPro.TMP_InputField input_name;
    public PlayerData playerData;
    public GameObject set_name_gui;
    public GameObject player_data_gui;
    public TMPro.TMP_Text player_name_text,win_text,lost_text;
    public TMPro.TMP_Text quit_gui_text;public Image BG;
    public Sprite title_alternative;
    void Start()
    {
        first_settong();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void first_settong(){
        Cursor.visible = true;
        bgm_title.time = 2.45f;
        if(playerData.player_name == "unknown"){
            set_name_gui.SetActive(true);
        }
        if(playerData.betraying_motherland){
            quit_gui_text.text = "To GULAG";
            BG.sprite = title_alternative;
        }
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
    public void enter_player_name(){
        playerData.player_name=input_name.text;
        set_name_gui.SetActive(false);
    }
    public void open_player_data_gui(){
        player_data_gui.SetActive(true);
        player_name_text.text = playerData.player_name;
        win_text.text = playerData.win_time.ToString();
        lost_text.text = playerData.lose_time.ToString();
    }
    public void close_player_data_gui(){
        player_data_gui.SetActive(false);

    }
    public void resetPlayerData(){
        player_data_gui.SetActive(false);
        playerData.player_name = "unknown";
        playerData.win_time = 0;
        playerData.lose_time = 0;
        playerData.betraying_motherland = false;
        set_name_gui.SetActive(true);
    }
}
