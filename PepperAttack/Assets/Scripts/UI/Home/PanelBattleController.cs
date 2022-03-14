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
    Button btnStart, btnNextBattle, btnSync;

    [SerializeField]
    Text txtMatchResult;
    [SerializeField]
    GameObject rewardPanel;
    [SerializeField]
    PanelItemCardController reward;

    [SerializeField]
    Image imgWinLose, imgWinLoseLight;

    [SerializeField]
    Sprite sprWin, sprLose, sprWinLight, sprLoseLight;

    string TeamID;

    private void Awake()
    {
        btnStart.onClick.AddListener(OnStartClickEvent);
        btnNextBattle.onClick.AddListener(OnNextBattleClickEvent);
        btnSync.onClick.AddListener(OnClickSyncEvent);
    }



    private void OnEnable()
    {
        PanelWaitingController.Instance.Init("Get Datas");
        PanelWaitingController.Instance.Show();
        GameRESTController.Instance.TeamController.MyTeamGet(OnGetTeamDone, OnGetTeamError);
        btnStart.interactable = false;
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
            btnStart.interactable = true;
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
        else
        {
            btnStart.interactable = false;
            for (int i = 0; i < peppersOnTeam.Length; i++)
            {
                // if (i < obj.data.peppers.Length)
                {
                    //  peppersOnTeam[i].Init(obj.data.peppers[i]);
                    peppersOnTeam[i].SetEmpty(true);
                    //  peppersOnTeam[i].ActiveRemoveButton(true);
                }
            }
        }
    }

    private void OnClickSyncEvent()
    {
        PanelWaitingController.Instance.Init("Sync");
        PanelWaitingController.Instance.Show();
        GameRESTController.Instance.TeamController.Sync(OnGetTeamDone, OnGetTeamError);
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
        imgWinLose.sprite = sprWin;
        imgWinLoseLight.sprite = sprWinLight;
        if (result == 1)
        {
            txtMatchResult.text = "YOU WIN";
        }
        if (result == 2)
            txtMatchResult.text = "DRAW";
        if (result == 3)
        {
            txtMatchResult.text = "YOU LOSE";
            imgWinLoseLight.sprite = sprLoseLight;
            imgWinLose.sprite = sprLose;
        }
        imgWinLose.SetNativeSize();
        rewardPanel.gameObject.SetActive(false);
        if (obj.data.match.reward != null && obj.data.match.reward.number > 0)
        {
            ///    txtReward.text = String.Format("Receive:{0} {1}", obj.data.match.reward.number, obj.data.match.reward.type);
            reward.gameObject.SetActive(true);
            rewardPanel.gameObject.SetActive(true);
            ItemData _Data = new ItemData() { number = obj.data.match.reward.number };
            reward.Init(_Data);
        }
        else
            reward.gameObject.SetActive(false);
    }
    private void OnNextBattleClickEvent()
    {
        panelMatchStart.gameObject.SetActive(true);
        panelMatchEnd.gameObject.SetActive(false);
    }
}
