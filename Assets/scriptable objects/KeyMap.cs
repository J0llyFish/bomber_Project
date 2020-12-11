﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KeyMap", menuName = "ScriptableObjects/KeyMap", order = 0)]
public class KeyMap : ScriptableObject
{
    //[Tooltip("forward")]
    public KeyCode switch_landing = KeyCode.G;
    public KeyCode plane_onward = KeyCode.W;
    public KeyCode plane_downward = KeyCode.S;
    public KeyCode plane_rotate_cw = KeyCode.A;
    public KeyCode plane_rotate_ccw = KeyCode.D;
    public KeyCode plane_rotate_shift_left = KeyCode.E;
    public KeyCode plane_rotate_shift_right = KeyCode.Q;
    public KeyCode plane_increase_power = KeyCode.LeftShift;
    public KeyCode plane_decrease_power = KeyCode.LeftControl;
    public KeyCode cgange_camera = KeyCode.C;
    public KeyCode drop_bomb = KeyCode.F;
}
