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
    }
    
    void Update()
    {
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
}
