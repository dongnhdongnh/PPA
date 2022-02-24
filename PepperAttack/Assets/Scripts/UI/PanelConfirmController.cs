using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PanelConfirmController : PanelBase<PanelConfirmController>
{
    [SerializeField]
    TextMeshProUGUI txt_title, txt_Content, txt_yes, txt_no;

    [SerializeField]
    Button btn_yes, btn_no;

    Action onYesEvent, onNoEvent;
    private void Awake()
    {
        btn_yes.onClick.AddListener(OnClickYesEvent);
        btn_no.onClick.AddListener(OnClickNoEvent);
    }
    public void Init(string title, string content,
        string yesContent = "yes", string noContent = "no",
        Action onYes = null, Action onNo = null)
    {
        this.txt_title.text = title;
        this.txt_Content.text = content;
        this.txt_yes.text = yesContent;
        this.txt_no.text = noContent;
        this.onNoEvent = onNo;
        this.onYesEvent = onYes;
    }

    public void InitConfirm(string title, string content, string okContent = "OK", Action onOk = null)
    {
        btn_yes.gameObject.SetActive(true);
        btn_no.gameObject.SetActive(false);
        Init(title, content, okContent, "", onOk);
    }

    void OnClickYesEvent()
    {
        this.onYesEvent?.Invoke();
        this.Hide();

    }
    void OnClickNoEvent()
    {
        this.onNoEvent?.Invoke();
        this.Hide();
    }
}
