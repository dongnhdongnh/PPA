using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PanelLeaderboardController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI[] txtTopRanks;
    [SerializeField]
    TextMeshProUGUI txtMyRanks;

    private void OnEnable()
    {
        PanelWaitingController.Instance.Init("Get Datas");
        PanelWaitingController.Instance.Show();
        GameRESTController.Instance.UserController.Leaderboard(OnLeaderboardDone, OnRESTError);
    }

    private void OnRESTError(string obj)
    {
        PanelWaitingController.Instance.Hide();
        PanelConfirmController.Instance.InitConfirm("Error", obj);
        PanelConfirmController.Instance.Show();
    }

    private void OnLeaderboardDone(HttpREsultObject obj)
    {
        PanelWaitingController.Instance.Hide();
        txtMyRanks.text = "#" + obj.data.my_position;
        for (int i = 0; i < txtTopRanks.Length; i++)
        {
            if (obj.data.top_players != null && i < obj.data.top_players.Length)
            {
                var _data = obj.data.top_players[i];
                txtTopRanks[i].text = String.Format("#{0}:{1}", i + 1, _data.user_info.username);
            }
            else
            {
                txtTopRanks[i].text = "";
            }
        }
    }
}
