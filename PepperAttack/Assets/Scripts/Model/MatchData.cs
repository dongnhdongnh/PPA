using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MatchData
{
    public MatchREsultData match;
    public MatchRewardData reward;
}

[System.Serializable]
public class MatchREsultData
{
    public int result;
}

[System.Serializable]
public class MatchRewardData
{
    public string type;
    public int number;
}
