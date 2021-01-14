using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserDefineKeyMap : MonoBehaviour
{
    public KeyMap keyMap;
    public List<Button> btn;
    public List<KeyCode> keyCodes;
    public bool isEditing;
    Button currBtn;
    int currBtnIndex;

    // Start is called before the first frame update
    void Start()
    {
        load_keys();
    }

    // Update is called once per frame
    void Update()
    {
        refreshUI();
    }

    void refreshUI()
    {
        //refresh
        for (int i=0;i< btn.Count ;i++)
        {
            btn[i].gameObject.GetComponentInChildren<TMPro.TMP_Text>().text = keyCodes[i].ToString();
        }
    }

    public void onClickButtom(Button inBtn)
    {
        isEditing = true;
        for (int i = 0; i < btn.Count; i++)
        {
            if (inBtn == btn[i])
            {
                currBtn = inBtn;
                currBtnIndex = i;
                break;
            }

        }
        switchBtnActive(false);

    }

    private void OnGUI()
    {
        if (isEditing)
        {
            if (Input.anyKeyDown)
            {
                if (Event.current.keyCode == KeyCode.Escape)
                {
                    isEditing = false;
                    switchBtnActive(true);
                }
                else if (Event.current.keyCode == KeyCode.None)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        keyCodes[currBtnIndex] = KeyCode.Mouse0;
                    }
                    else if (Input.GetMouseButtonDown(1))
                    {
                        keyCodes[currBtnIndex] = KeyCode.Mouse1;
                    }
                    isEditing = false;
                    switchBtnActive(true);
                }
                else
                {
                    keyCodes[currBtnIndex] = Event.current.keyCode;
                    //Debug.Log(Event.current.keyCode);
                    isEditing = false;
                    switchBtnActive(true);
                }
            }
            save_keys();
        }
    }

    void switchBtnActive(bool switcher)
    {
        for (int i = 0; i < btn.Count; i++)
        {
            btn[i].interactable = switcher;
        }
    }

    void load_keys(){
        keyCodes = new List<KeyCode>();
        keyCodes.Add(keyMap.return_to_menu);
        keyCodes.Add(keyMap.control_key);
        keyCodes.Add(keyMap.switch_landing);
        keyCodes.Add(keyMap.plane_onward);
        keyCodes.Add(keyMap.plane_downward);
        keyCodes.Add(keyMap.plane_rotate_cw);
        keyCodes.Add(keyMap.plane_rotate_ccw);
        keyCodes.Add(keyMap.plane_rotate_shift_left);
        keyCodes.Add(keyMap.plane_rotate_shift_right);
        keyCodes.Add(keyMap.plane_increase_power);
        keyCodes.Add(keyMap.plane_decrease_power);
        keyCodes.Add(keyMap.cgange_camera);
        keyCodes.Add(keyMap.drop_bomb);
        keyCodes.Add(keyMap.toggle_map);
        keyCodes.Add(keyMap.toggle_radio_volume);
        keyCodes.Add(keyMap.radio_frequency_up);
        keyCodes.Add(keyMap.radio_frequency_down);
    }
    void save_keys(){
        keyMap.return_to_menu = keyCodes[0];
        keyMap.control_key = keyCodes[1];
        keyMap.switch_landing = keyCodes[2];
        keyMap.plane_onward = keyCodes[3];
        keyMap.plane_downward = keyCodes[4];
        keyMap.plane_rotate_cw = keyCodes[5];
        keyMap.plane_rotate_ccw = keyCodes[6];
        keyMap.plane_rotate_shift_left = keyCodes[7];
        keyMap.plane_rotate_shift_right = keyCodes[8];
        keyMap.plane_increase_power = keyCodes[9];
        keyMap.plane_decrease_power = keyCodes[10];
        keyMap.cgange_camera = keyCodes[11];
        keyMap.drop_bomb = keyCodes[12];
        keyMap.toggle_map = keyCodes[13];
        keyMap.toggle_radio_volume = keyCodes[14];
        keyMap.radio_frequency_up = keyCodes[15];
        keyMap.radio_frequency_down = keyCodes[16];
    }
}
