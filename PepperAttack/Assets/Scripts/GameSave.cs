using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSave
{
    public static string Cache_UserName
    {
        get
        {
            return PlayerPrefs.GetString(GameSaveKey.KEY_USERNAME, "");
        }
        set
        {
            PlayerPrefs.SetString(GameSaveKey.KEY_USERNAME, value);
            PlayerPrefs.Save();
        }
    }

    public static string Cache_Password
    {
        get
        {
            return PlayerPrefs.GetString(GameSaveKey.KEY_PASSWORD, "");
        }
        set
        {
            PlayerPrefs.SetString(GameSaveKey.KEY_PASSWORD, value);
            PlayerPrefs.Save();
        }
    }
}

public class GameSaveKey
{
    public const string KEY_USERNAME = "KEY_USERNAME";
    public const string KEY_PASSWORD = "KEY_PASSWORD";
}