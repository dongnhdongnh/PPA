using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardData
{

}


[System.Serializable]
public class LeaderboardDataTopPlayers
{
    public string id;
    public int elo;
    public LeaderboardDataUserInfo user_info;
}

[System.Serializable]
public class LeaderboardDataUserInfo
{
    public string user_id;
    public string username;
}