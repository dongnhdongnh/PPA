using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HomeSceneController : MonoBehaviour
{
    [SerializeField]
    Color colorSelect;
    [SerializeField]
    Button[] btnSelects;
    [SerializeField]
    GameObject[] panelSelects;

    private void Awake()
    {
        GameUtils.EventHandlerIni();
        for (int i = 0; i < btnSelects.Length; i++)
        {
            int index = i;
            btnSelects[i].onClick.AddListener(() =>
            {

                SelectPanel(index);
            });
        }
        SelectPanel(0);
    }

    private void OnDestroy()
    {
        GameUtils.EventHandlerReset();
    }

    void SelectPanel(int index)
    {
        foreach (var item in panelSelects)
        {
            item.SetActive(false);
        }
        panelSelects[index].SetActive(true);
        foreach (var item in btnSelects)
        {
            item.image.color = Color.white;
            item.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        }
        btnSelects[index].image.color = colorSelect;
        btnSelects[index].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;

    }
}
