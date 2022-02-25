using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTeamController : MonoBehaviour
{
    [SerializeField]
    RectTransform panelListView;

    [SerializeField]
    PanelPepperCardController prefabPeppersOnList;
    [SerializeField]
    PanelPepperCardController[] peppersOnTeam;

    private void OnEnable()
    {
        GameRESTController.Instance.TeamController.MyTeamGet(OnGetTeamDone, Debug.LogError);
        GameRESTController.Instance.TeamController.AllPepper(OnGetAllDone, Debug.LogError);
    }

    private void OnGetTeamDone(HttpREsultObject obj)
    {
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
            }

        }

    }
}
