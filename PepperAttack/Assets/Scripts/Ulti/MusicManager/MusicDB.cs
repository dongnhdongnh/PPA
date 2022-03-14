using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDB : MonoBehaviour
{
    public AudioClip[] Music_Home;

    public AudioClip Music_Home_Random
    {
        get
        {
            return Music_Home[Random.Range(0, Music_Home.Length)];
        }
    }

    //public AudioClip Music_InGame, Music_Loading, SFX_button, Music_Lose, Music_Win;
    public AudioClip SFX_button, SFX_popup;
    //public AudioClip SFX_EnemyDie;
}
