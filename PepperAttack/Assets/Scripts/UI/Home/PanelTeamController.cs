using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelTeamController : MonoBehaviour
{
    [SerializeField]
    RectTransform panelListView;

    [SerializeField]
    PanelPepperCardController prefabPeppersOnList;
    [SerializeField]
    PanelPepperCardController[] peppersOnTeam;

    [Header("Info")]
    [SerializeField]
    GameObject panelInfo;
    [SerializeField]
    Image imgHP, imgAtk, imgDef, imgCrit, imgEva;
    [SerializeField]
    TextMeshProUGUI txtInfoName;
    [SerializeField]
    Button btnAddTeam;

    private void Awake()
    {
        btnAddTeam.onClick.AddListener(OnAddTeamClickEvent);
    }



    private void OnEnable()
    {
        PanelWaitingController.Instance.Init("Get Datas");
        PanelWaitingController.Instance.Show();
        dataCurrent = null;
        panelInfo.gameObject.SetActive(false);

        foreach (Transform child in panelListView.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        GameRESTController.Instance.TeamController.MyTeamGet(OnGetTeamDone, OnRESTError);
        GameRESTController.Instance.TeamController.AllPepper(OnGetAllDone, Debug.LogError);
    }

    private void OnGetTeamDone(HttpREsultObject obj)
    {
        PanelWaitingController.Instance.Hide();
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
                    peppersOnTeam[i].ActiveRemoveButton(true);
                    peppersOnTeam[i].SetSelectAction((p) => ShowInfo(p));
                    int index = i;
                    peppersOnTeam[i].SetRemoveAction((p) =>
                    {
                        peppersOnTeam[index].ClearData();
                        UpdateTeam();
                    });
                }
            }
        }
    }

    private void OnGetAllDone(HttpREsultObject obj)
    {
        foreach (Transform child in panelListView.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        if (obj.data.peppers != null && obj.data.peppers.Length > 0)
        {
            foreach (PepperData data in obj.data.peppers)
            {
                PanelPepperCardController peper = Instantiate(prefabPeppersOnList, panelListView);
                peper.Init(data);
                peper.SetSelectAction((p) => ShowInfo(p));
            }

        }
    }
    PepperData dataCurrent;
    void ShowInfo(PepperData data)
    {
        dataCurrent = data;
        panelInfo.gameObject.SetActive(true);
        imgHP.rectTransform.localScale = new Vector3((float)data.pepper_stat.hp / 100.0f, 1, 1);
        imgAtk.rectTransform.localScale = new Vector3((float)data.pepper_stat.attack / 100.0f, 1, 1);
        imgDef.rectTransform.localScale = new Vector3((float)data.pepper_stat.defense / 100.0f, 1, 1);
        imgCrit.rectTransform.localScale = new Vector3((float)data.pepper_stat.crit / 100.0f, 1, 1);
        imgEva.rectTransform.localScale = new Vector3((float)data.pepper_stat.eva / 100.0f, 1, 1);
        txtInfoName.text = data.pepper_stat.pepper_id;

        bool inTeam = false;
        int countTeam = 0;
        foreach (var item in peppersOnTeam)
        {
            if (item.Data != null && item.Data.pepper_stat.pepper_id.Equals(data.pepper_stat.pepper_id))
                inTeam = true;
            if (item.Data != null)
                countTeam++;
        }

        btnAddTeam.gameObject.SetActive(countTeam < 3 && !inTeam);
    }
    private void OnAddTeamClickEvent()
    {
        if (dataCurrent == null) return;
        bool isUpdate = false;
        foreach (var item in peppersOnTeam)
        {
            if (item.Data == null)
            {
                item.Init(dataCurrent);
                isUpdate = true;
                break;
            }
        }
        if (isUpdate)
            UpdateTeam();
    }

    public void UpdateTeam()
    {
        PanelWaitingController.Instance.Init("Update Datas");
        PanelWaitingController.Instance.Show();
        List<string> pps = new List<string>();
        foreach (var item in peppersOnTeam)
        {
            if (item.Data != null)
                pps.Add(item.Data.pepper_stat.pepper_id);
            else
                pps.Add("");
        }

        GameRESTController.Instance.TeamController.MyTeamUpdate(pps, OnUpdateTeamDone, OnRESTError);
    }

    private void OnUpdateTeamDone(HttpREsultObject obj)
    {
        PanelWaitingController.Instance.Hide();
        GameRESTController.Instance.TeamController.MyTeamGet(OnGetTeamDone, Debug.LogError);
        GameRESTController.Instance.TeamController.AllPepper(OnGetAllDone, Debug.LogError);
    }

    private void OnRESTError(string obj)
    {
        PanelWaitingController.Instance.Hide();
        PanelConfirmController.Instance.InitConfirm("Error", obj);
        PanelConfirmController.Instance.Show();
    }

}
