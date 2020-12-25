using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioControl : MonoBehaviour
{
    public AudioSource channel_1,channel_2,noise;
    public float radio_resolution = 1f;
    public float frequency_curr = 100f;
    public float ch_1_frequency;
    public float ch_2_frequency;
    public float overall_volume = 1;
    void Start()
    {
        // specialized for audio sources
        channel_1.time = 10;
        channel_2.time = 40;
        //
        set_volume();
    }
    
    void Update()
    {
        radio_control();
        overall_volume = Mathf.Clamp01(overall_volume);
        set_volume();
    }

    float ch_1_v,ch_2_v,noise_v;
    void set_volume(){
        ch_1_v = Mathf.Exp(-Mathf.Abs((frequency_curr-ch_1_frequency)/radio_resolution)); 
        ch_2_v = Mathf.Exp(-Mathf.Abs((frequency_curr-ch_2_frequency)/radio_resolution));
        noise_v = Mathf.Clamp01(1-ch_2_v-ch_1_v);

        channel_1.volume = ch_1_v * overall_volume;
        channel_2.volume = ch_2_v * overall_volume;
        noise.volume = noise_v * overall_volume;
    }

    void radio_control(){
        if(Input.GetKeyDown(GameController.gameController.keyMap.toggle_radio_volume)){
            if(overall_volume > 0.8){
                overall_volume = 0;
            }else if(overall_volume < 0.2){
                overall_volume = 0.5f;
            }else{
                overall_volume = 1;
            }
        }
        if(Input.GetKey(GameController.gameController.keyMap.radio_frequency_up)){
            frequency_curr += Time.deltaTime * 15f;
        }
        if(Input.GetKey(GameController.gameController.keyMap.radio_frequency_down)){
            frequency_curr -= Time.deltaTime * 15f;
        }
        frequency_curr = Mathf.Clamp(frequency_curr,10,150);
    }
}
