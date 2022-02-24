using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "GameUnityData", menuName = "Assets/Scriptable Objects/GameUnityData")]
public class GameUnityData : ScriptableObject
{

    #region Constants
    #endregion
    //========================================================
    #region Members
    private static GameUnityData mInstance;
    public static GameUnityData instance
    {
        get
        {
            if (mInstance == null)
                mInstance = Resources.Load<GameUnityData>("Collection/GameUnityData");
            return mInstance;
        }
    }

    [Space(3)]
    [Header("Character Effect")]
    public GameObject Effect_stun;
    public GameObject Effect_Burn, Effect_Freeze,
        Effect_Slow, Effect_Silence, Effect_horrify, Effect_shield,
        Effect_bleed, Effect_poison, Effect_buff, Effect_neft, Effect_HPRefill;


    public AutoDespawnObject Text_ingame;




    [SerializeField]
    GameRemoteConfig gameRemoteConfig;

    //public bool active_Hard_LevelConfigData;
 
    [Button("ShowRemoteConfig")]
    public void ShowReoteConfigData()
    {
        Debug.LogError("COPY " + JsonUtility.ToJson(this.gameRemoteConfig));
        GUIUtility.systemCopyBuffer = JsonUtility.ToJson(this.gameRemoteConfig);
    }


    public string PVE_BASE_REWARD_RAW;
    public int[] PVE_BASE_REWARD;
    [ReadOnly] public int[] PVE_BASE_EXP;
    [Button("Convert Data")]
    public void ConvertDataInput()
    {
        List<int> _raws = new List<int>();
        string[] raws = PVE_BASE_REWARD_RAW.Replace(' ', ',').Replace('\n', ',').Split(',');
        foreach (var item in raws)
        {
            _raws.Add(int.Parse(item));
        }
        PVE_BASE_REWARD = _raws.ToArray();
        // Debug.LogError("new data " + PVE_BASE_REWARD_RAW.Replace(' ', ',').Replace('\n', ','));
        //GUIUtility.systemCopyBuffer = JsonUtility.ToJson(this.gameRemoteConfig);
    }

    // [Button("Convert PVE base exp")]
    // public void ConvertPvEBaseExp()
    // {
    //     List<int> _raws = new List<int>();
    //     for (int i = 0; i < 96; i++)
    //     {
    //         _raws.Add(100+i*20);
    //     }
    //     PVE_BASE_EXP = _raws.ToArray();
    // }

    public int GetPvEBaseExp(int currentID)
    {
        return PVE_BASE_EXP[Mathf.Clamp(currentID - 1, 0, PVE_BASE_EXP.Length - 1)];
    }
    #endregion
    //========================================================
    #region Unity Funtions
    #endregion
    //========================================================
    #region Private
    #endregion
    //========================================================
    #region Public
  
    #endregion

    [Space(10)]
    [Header("===============Game Setting===============")]
    [Tooltip("Mỗi normal attack bắn trúng kẻ địch sẽ regen x Mana")]
    public int mana_NormalAttack_nonCyber = 10;
    [Tooltip("Mỗi normal attack bắn trúng Cyber sẽ regen x Mana")]
    public int mana_NormalAttack_cyber = 20;
    [Tooltip("Mỗi lần last hit kẻ địch là Cyber sẽ được regen x Mana")]
    public int mana_LastHit_Cyber = 300;
    [Tooltip("Mỗi lần last hit kẻ địch là nonCyber sẽ được regen x Mana")]
    public int mana_LastHit_NonCyber = 1;

    [Tooltip("Mỗi lần bị đánh trúng bởi normal attack của đối thủ là Cyber khác sẽ regen x Mana")]
    public int mana_TakeDam_NormalAtk_Cyber = 1;
    [Tooltip("Mỗi lần bị đánh trúng bởi skill 1 của đối thủ là Cyber khác sẽ regen x Mana")]
    public int mana_TakeDam_SkillAtk_Cyber = 100;
    [Tooltip("Mỗi lần bị đánh trúng bởi normal attack hoặc skill của đối thủ là Unit sẽ regen x Mana")]
    public int mana_TakeDam_NonCyber = 1;
    [Tooltip("Mỗi lần bị đánh trúng bởi skill 1 của đối thủ là nonCyber khác sẽ regen x Mana")]
    public int mana_TakeDam_SkillAtk_NonCyber = 2;
    [Space(10)]
    public int mIDLECost_autoplay_PvE = 500;


    [Space(10)]
    [Header("===============Server Setting===============")]
    public RESTDebugData restDebugData;
    public bool restShowDetail = false;
    public ServerHostType SeverHost;
    public bool RESTDebug;

    [Space(10)]
    [Header("===============Test Setting===============")]
    public bool TestHeroMode;
    public int[] TestHeroIndex;
    public bool TestLocalMap;
    public bool TestPvP_playWithDEF;
    public bool TestPassiveUnlockAll;

    public bool gayMode { get; set; }
}

public enum ServerHostType
{
    Local, GG_Clound, GG_Clound_SSL, AWS, Test,Product
};
[System.Serializable]
public class GameRemoteConfig
{
    public float Speed_ingamex1;
    public float Speed_ingamex2;
    public float Speed_PvP;
    public bool active_ABLevel;
    public bool active_LevelConfigData;


    public bool ads_inter_active;
    [Tooltip("Tần suất hiển thị mỗi x (x cấu hình, x mặc định = 1) ván 1 lần")]
    public int ads_inter_showPerMission;
    [Tooltip("Điều kiện hiển thị: Sau lvl x")]
    public int ads_inter_showAfterMission;
    [Tooltip("Thời gian tối thiểu giữa 2 lần show x s")]
    public float ads_inter_showAfterTime;


    public bool Function_ShowMap;
    public bool Function_ChangeFormationInMissionDetail;
    public bool Function_TapToShot_Active;
    public bool Function_TapToShot_CanHold;
    public bool Function_PvP_Active;
    public bool Function_Autoplay_Active;
    public int Function_Autoplay_ActiveLevel;
}



