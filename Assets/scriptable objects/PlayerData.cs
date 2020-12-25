using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public string player_name = "unknown";
    //public int exp = 0;
    public int win_time = 0;
    public int lose_time = 0;
    public bool betraying_motherland = false;
}
