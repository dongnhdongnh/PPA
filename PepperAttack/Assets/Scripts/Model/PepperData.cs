using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PepperData
{
    public string id;
    public string nft_id;
    public int type;

    public Pepper_NFTinfo nft_info;
    public Pepper_Stat pepper_stat;
    public Pepper_mystic_info pepper_mystic_info;

    public PepperClass Class
    {
        get
        {
            string _value = pepper_mystic_info.attributesValue("Class");
            PepperClass _output;
            if (Enum.TryParse(_value, out _output))
            {
               // Debug.LogError(_output.ToString());
                return _output;
            }
            else return PepperClass.none;
        }
    }
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

[System.Serializable]
public class Pepper_mystic_info
{
    public string image;
    public Pepper_mystic_attributes[] attributes;
    public string attributesValue(string key)
    {
        for (int i = 0; i < attributes.Length; i++)
        {
            if (attributes[i].trait_type.Equals(key))
                return attributes[i].value;
        }
        return null;
    }
}

[System.Serializable]
public class Pepper_mystic_attributes
{
    public string trait_type;
    public string value;
}

public enum PepperClass
{
    Bell = 0, Chilli = 1, Ghost = 2, Habanero = 3, Purira = 4, Reaper = 5, none = -1
}


