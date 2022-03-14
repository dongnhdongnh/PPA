using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PanelLeaderboardController : MonoBehaviour
{
    [SerializeField]
    PanelRankLineController[] txtTopRanks;
    [SerializeField]
    PanelRankLineController txtMyRanks;
    [SerializeField]
    TextMeshProUGUI txtSSEnd;

    private void OnEnable()
    {
        PanelWaitingController.Instance.Init("Get Datas");
        PanelWaitingController.Instance.Show();
        GameRESTController.Instance.UserController.Leaderboard(OnLeaderboardDone, OnRESTError);
        GameRESTController.Instance.UserController.Home(OnLoadItemsDone, OnRESTError);
     txtSSEnd.text = "--";
    }
    private void FixedUpdate()
    {
        txtSSEnd.text = "Season end in " + GameUtils.StringServerToDate(_time);
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
        txtMyRanks.Init(obj.data.my_position, "You", "0");

        // txtMyRanks.text = "#" + obj.data.my_position;
        for (int i = 0; i < txtTopRanks.Length; i++)
        {
            if (obj.data.top_players != null && i < obj.data.top_players.Length)
            {
                var _data = obj.data.top_players[i];
                txtTopRanks[i].gameObject.SetActive(true);
                // txtTopRanks[i].text = String.Format("#{0}:{1}", i + 1, _data.user_info.username);
                txtTopRanks[i].Init(i + 1, _data.user_info.username, "0");
            }
            else
            {
                //   txtTopRanks[i].text = "";
                txtTopRanks[i].gameObject.SetActive(false);
            }
        }
    }
    string _time;
    private void OnLoadItemsDone(HttpREsultObject obj)
    {
        PanelWaitingController.Instance.Hide();

        _time = obj.data.season[0].end_at;
    }
}
