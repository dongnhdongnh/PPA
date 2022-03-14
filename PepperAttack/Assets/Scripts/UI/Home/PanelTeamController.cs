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
    List<PanelPepperCardController> peppersAll = new List<PanelPepperCardController>();
    [SerializeField]
    PanelPepperCardController[] peppersOnTeam;

    [Header("Info")]
    [SerializeField]
    GameObject panelInfo;
    [SerializeField]
    Image imgPepperView;
    [SerializeField]
    Text imgHP, imgAtk, imgDef, imgCrit, imgEva;
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
        foreach (var item in peppersOnTeam)
        {
            item.ClearData();
        }
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
        else
        {
            for (int i = 0; i < peppersOnTeam.Length; i++)
            {
                // if (i < obj.data.peppers.Length)
                {
                    peppersOnTeam[i].SetEmpty(true);
                }
            }
        }
        UpdateTick();

    }

    private void OnGetAllDone(HttpREsultObject obj)
    {
        foreach (Transform child in panelListView.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        peppersAll.Clear();
        if (obj.data.peppers != null && obj.data.peppers.Length > 0)
        {
            foreach (PepperData data in obj.data.peppers)
            {
                PanelPepperCardController peper = Instantiate(prefabPeppersOnList, panelListView);
                peper.Init(data);
                peper.SetSelectAction((p) => ShowInfo(p));
                peppersAll.Add(peper);
            }
        }
        UpdateTick();
    }

    void UpdateTick()
    {
        if (peppersAll != null)
            foreach (PanelPepperCardController peper in peppersAll)
            {
                if (peppersOnTeam != null)
                    foreach (var _onTeam in peppersOnTeam)
                    {
                        if (peper.Data != null && _onTeam.Data != null && peper.Data.id.Equals(_onTeam.Data.id))
                        {
                            if (peper != null)
                                peper.ShowTick();
                            break;
                        }
                    }
            }
    }

    PepperData dataCurrent;
    void ShowInfo(PepperData data)
    {
        dataCurrent = data;
        panelInfo.gameObject.SetActive(true);

        foreach (PanelPepperCardController item in peppersAll)
        {
            item.SetSelect(data.Equals(item.Data));
        }
        //imgHP.rectTransform.localScale = new Vector3((float)data.pepper_stat.hp / 100.0f, 1, 1);
        //imgAtk.rectTransform.localScale = new Vector3((float)data.pepper_stat.attack / 100.0f, 1, 1);
        //imgDef.rectTransform.localScale = new Vector3((float)data.pepper_stat.defense / 100.0f, 1, 1);
        //imgCrit.rectTransform.localScale = new Vector3((float)data.pepper_stat.crit / 100.0f, 1, 1);
        //imgEva.rectTransform.localScale = new Vector3((float)data.pepper_stat.eva / 100.0f, 1, 1);

        imgPepperView.sprite = GameUnityData.instance.HeroView(data.Class);

        imgHP.text = data.pepper_stat.hp.ToString("00");
        imgAtk.text = data.pepper_stat.attack.ToString("00");
        imgDef.text = data.pepper_stat.defense.ToString("00");
        imgCrit.text = data.pepper_stat.crit.ToString("00");
        imgEva.text = data.pepper_stat.eva.ToString("00");

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

        btnAddTeam.gameObject.SetActive(countTeam < 5 && !inTeam);
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
