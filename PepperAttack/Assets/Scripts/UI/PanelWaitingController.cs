using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public class PanelWaitingController : PanelBase<PanelWaitingController>
{
    [SerializeField]
    TextMeshProUGUI txt_text;

    string _currentText;
    public void Init(string text)
    {
        _currentText = text;

    }

    private void Update()
    {
        txt_text.text = _currentText;
        int _time = (int)Time.time;
        for (int i = 0; i < _time % 4; i++)
            txt_text.text += ".";
    }
}
