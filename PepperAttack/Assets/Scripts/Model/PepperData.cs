using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PepperData
{
    public string id;
    public string nft_id;

    public Pepper_NFTinfo nft_info;
    public Pepper_Stat pepper_stat;
}


[System.Serializable]
public class Pepper_NFTinfo
{
    public string id;
    public string metadata;

}

[System.Serializable]
public class Pepper_Stat
{
    public string pepper_id;
    public int attack;
    public int defense;
    public int crit;
    public int eva;
    public int hp;
}
