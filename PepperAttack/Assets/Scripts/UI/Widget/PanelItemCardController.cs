using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PanelItemCardController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI txt_Number;
    [SerializeField]
    Button btn_use;


    public ItemData Data { get; set; }

    private void Awake()
    {
        btn_use.onClick.AddListener(OnUseClickEvent);
    }

    public void Init(ItemData data)
    {
        this.txt_Number.text = data.number.ToString();
    }

    private void OnUseClickEvent()
    {
        GameEvent.InventoryItemUse.Instance.Data = this.Data;
        GameUtils.RaiseMessage(GameEvent.InventoryItemUse.Instance);
    }
}
