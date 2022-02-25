using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelBattleController : MonoBehaviour
{
    [SerializeField]
    GameObject panelMatchStart, panelMatchEnd;

    [SerializeField]
    PanelPepperCardController[] peppersOnTeam;

    [SerializeField]
    Button btnStart, btnNextBattle;

    [SerializeField]
    TextMeshProUGUI txtMatchResult, txtReward;

    string TeamID;

    private void Awake()
    {
        btnStart.onClick.AddListener(OnStartClickEvent);
        btnNextBattle.onClick.AddListener(OnNextBattleClickEvent);
    }



    private void OnEnable()
    {
        PanelWaitingController.Instance.Init("Get Datas");
        PanelWaitingController.Instance.Show();
        GameRESTController.Instance.TeamController.MyTeamGet(OnGetTeamDone, OnGetTeamError);
    }

    private void OnGetTeamError(string obj)
    {
        PanelWaitingController.Instance.Hide();
        PanelConfirmController.Instance.InitConfirm("Error", obj);
        PanelConfirmController.Instance.Show();
    }
    private void OnGetTeamDone(HttpREsultObject obj)
    {
        PanelWaitingController.Instance.Hide();
        TeamID = obj.data.team.id;
        if (obj.data.peppers != null && obj.data.peppers.Length > 0)
        {
            for (int i = 0; i < peppersOnTeam.Length; i++)
            {
                peppersOnTeam[i].SetEmpty(true);
                peppersOnTeam[i].ActiveRemoveButton(false);
            }

            for (int i = 0; i < peppersOnTeam.Length; i++)
            {
                if (i < obj.data.peppers.Length)
                {
                    peppersOnTeam[i].Init(obj.data.peppers[i]);
                    peppersOnTeam[i].SetEmpty(false);
                    //  peppersOnTeam[i].ActiveRemoveButton(true);
                }
            }
        }
    }

    private void OnStartClickEvent()
    {
        PanelWaitingController.Instance.Init("Start Battle");
        PanelWaitingController.Instance.Show();
        GameRESTController.Instance.BattleController.StartMatch(TeamID, OnMatchDone, OnMatchError);
    }

    private void OnMatchError(string obj)
    {
        PanelWaitingController.Instance.Hide();
        PanelConfirmController.Instance.InitConfirm("Error", obj);
        PanelConfirmController.Instance.Show();
    }


    private void OnMatchDone(HttpREsultObject obj)
    {
        PanelWaitingController.Instance.Hide();
        panelMatchStart.gameObject.SetActive(false);
        panelMatchEnd.gameObject.SetActive(true);

        int result = obj.data.match.match.result;
        if (result == 1)
            txtMatchResult.text = "YOU WIN";
        if (result == 2)
            txtMatchResult.text = "DRAW";
        if (result == 3)
            txtMatchResult.text = "YOU LOSE";
        if (obj.data.match.reward != null && obj.data.match.reward.number > 0)
            txtReward.text = String.Format("Receive:{0} {1}", obj.data.match.reward.number, obj.data.match.reward.type);
        else
            txtReward.text = "";
    }
    private void OnNextBattleClickEvent()
    {
        panelMatchStart.gameObject.SetActive(true);
        panelMatchEnd.gameObject.SetActive(false);
    }
}
