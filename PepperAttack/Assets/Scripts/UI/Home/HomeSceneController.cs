using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeSceneController : MonoBehaviour
{
    private void Awake()
    {
        GameUtils.EventHandlerIni();
    }

    private void OnDestroy()
    {
        GameUtils.EventHandlerReset();
    }
}
