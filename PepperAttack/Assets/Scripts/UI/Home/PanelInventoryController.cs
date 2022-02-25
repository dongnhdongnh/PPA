using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelInventoryController : MonoBehaviour
{

    [SerializeField]
    PanelItemCardController ItemCard;

    [Header("Heroes Select")]
    [SerializeField]
    GameObject panel_HeroList;
    [SerializeField]
    RectTransform panelHeroListView;
    [SerializeField]
    PanelPepperCardController prefabPeppersOnList;
    [SerializeField]
    Button btnCancelSelect;

    List<PanelPepperCardController> pepperAlls;

    private void Awake()
    {
        btnCancelSelect.onClick.AddListener(OnCancelSelectClickEvent);
    }

    private void OnCancelSelectClickEvent()
    {
        PanelWaitingController.Instance.Init("Get Datas");
        PanelWaitingController.Instance.Show();
        GameRESTController.Instance.InventoryController.AllItems(OnLoadItemsDone, OnRESTError);
        panel_HeroList.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        PanelWaitingController.Instance.Init("Get Datas");
        PanelWaitingController.Instance.Show();
        GameRESTController.Instance.InventoryController.AllItems(OnLoadItemsDone, OnRESTError);
        GameRESTController.Instance.TeamController.AllPepper(OnGetTeamDone, Debug.LogError);
        GameUtils.AddHandler<GameEvent.InventoryItemUse>(OnInventoryItemUseEvent);
    }
    private void OnInventoryItemUseEvent(GameEvent.InventoryItemUse obj)
    {
        panel_HeroList.gameObject.SetActive(true);
    }

    private void OnSelectPepper(PepperData data)
    {
        Debug.LogError("Select " + data.pepper_stat.pepper_id);
        PanelWaitingController.Instance.Init("Use item for " + data.pepper_stat.pepper_id);
        PanelWaitingController.Instance.Show();
        GameRESTController.Instance.InventoryController.UseItem(data.pepper_stat.pepper_id, OnUseItemDone, OnRESTError);
    }

    private void OnRESTError(string obj)
    {
        PanelWaitingController.Instance.Hide();
        PanelConfirmController.Instance.InitConfirm("Error", obj);
        PanelConfirmController.Instance.Show();
    }

    private void OnUseItemDone(HttpREsultObject obj)
    {
        PanelWaitingController.Instance.Hide();
        if (obj.data.resultUseHp != null)
        {
            foreach (PanelPepperCardController item in pepperAlls)
            {
                if (item.Data.pepper_stat.pepper_id.Equals(obj.data.resultUseHp.pepper_id))
                {
                    item.UpdateHP(obj.data.resultUseHp.hp);
                    break;
                }
            }
        }
    }

    #region RESTCallback
    private void OnGetTeamDone(HttpREsultObject obj)
    {
        pepperAlls = new List<PanelPepperCardController>();
        foreach (Transform child in panelHeroListView.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        if (obj.data.peppers != null && obj.data.peppers.Length > 0)
        {
            foreach (PepperData data in obj.data.peppers)
            {
                PanelPepperCardController peper = Instantiate(prefabPeppersOnList, panelHeroListView);
                peper.Init(data);
                peper.SetSelectAction((pepper) =>
                {
                    OnSelectPepper(pepper);
                });
                pepperAlls.Add(peper);
            }

        }

    }

    private void OnLoadItemsDone(HttpREsultObject obj)
    {
        PanelWaitingController.Instance.Hide();
        ItemData[] items = obj.data.potions;
        if (items != null && items.Length > 0)
        {
            ItemCard.Init(items[0]);
            //ItemCard.On
        }
        //      throw new NotImplementedException();
    }

    #endregion
}
