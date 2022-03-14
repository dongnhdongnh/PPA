using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PanelPepperCardController : MonoBehaviour
{
    [SerializeField]
    Text txtName;
    [SerializeField]
    Image imgAvatar;
    [SerializeField]
    GameObject panelEmpty, panelFull;
    [SerializeField]
    Image imgHPProcess;
    [SerializeField]
    Button btnSelect, btnRemove;
    [SerializeField]
    GameObject objTick, objSelect;

    public PepperData Data { get; set; }

    System.Action<PepperData> OnSelectClickAction, OnRemoveClickAction;

    private void Awake()
    {
        btnRemove.onClick.AddListener(OnRemoveClickEvent);
        btnSelect.onClick.AddListener(OnSelectClickEvent);
    }

    public void Init(PepperData data)
    {
        this.Data = data;
        imgAvatar.sprite = GameUnityData.instance.HeroAvatar(data.Class);
        string _start = data.pepper_stat.pepper_id.Substring(0, 3);
        string _end = data.pepper_stat.pepper_id.Substring(data.pepper_stat.pepper_id.Length - 3, 3);
        txtName.text = _start + "..." + _end;
        imgHPProcess.rectTransform.localScale = new Vector3(((float)data.pepper_stat.hp) / 100.0f, 1, 1);
        objTick.SetActive(false);
    }
    public void SetSelect(bool isSelect)
    {
        objSelect.SetActive(isSelect);
    }
    public void ShowTick()
    {
        objTick.SetActive(true);
    }

    public void SetEmpty(bool isEmpty)
    {
        panelEmpty.gameObject.SetActive(isEmpty);
        panelFull.gameObject.SetActive(!isEmpty);
    }
    public void ActiveRemoveButton(bool isActive)
    {
        btnRemove.gameObject.SetActive(isActive);
    }
    public void UpdateHP(int hp)
    {
        Data.pepper_stat.hp = hp;
        imgHPProcess.rectTransform.localScale = new Vector3(((float)Data.pepper_stat.hp) / 100.0f, 1, 1);
    }

    public void SetSelectAction(System.Action<PepperData> OnSelect)
    {
        this.OnSelectClickAction = OnSelect;
    }
    public void SetRemoveAction(System.Action<PepperData> OnSelect)
    {
        this.OnRemoveClickAction = OnSelect;
    }
    private void OnRemoveClickEvent()
    {
        OnRemoveClickAction?.Invoke(Data);
    }
    private void OnSelectClickEvent()
    {
        OnSelectClickAction?.Invoke(Data);
    }

    public void ClearData()
    {
        this.Data = null;
        SetEmpty(true);
    }

}
