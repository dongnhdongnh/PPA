using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelHomeController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI txtHPTime, txtSSName, txtSSTime;
    [SerializeField]
    TextMeshProUGUI txtNewName, txtNewContent;

    private void OnEnable()
    {
        PanelWaitingController.Instance.Init("Get Datas");
        PanelWaitingController.Instance.Show();
        GameRESTController.Instance.UserController.Home(OnLoadItemsDone, OnRESTError);
    }

    private void OnRESTError(string obj)
    {
        PanelWaitingController.Instance.Hide();
        PanelConfirmController.Instance.InitConfirm("Error", obj);
        PanelConfirmController.Instance.Show();
    }

    private void OnLoadItemsDone(HttpREsultObject obj)
    {
        PanelWaitingController.Instance.Hide();
        txtHPTime.text = obj.data.next_generate_hp.ToString();
        txtNewName.text = obj.data.news.title;
        txtNewContent.text = obj.data.news.description;
        txtSSName.text = obj.data.season[0].name;
        txtSSTime.text = obj.data.season[0].end_at;
    }
}
