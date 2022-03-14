using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HomeSceneController : MonoBehaviour
{
    [Header("Select")]
    [SerializeField]
    Sprite sprNormal, sprSelect;
    [SerializeField]
    Button[] btnSelects;
    [SerializeField]
    GameObject[] panelSelects;

    [Header("Other")]
    [SerializeField]
    Button btnMusic, btnSFX, btnQuit;
    [SerializeField]
    Image imgMusic, imgSFX;
    [SerializeField]
    Sprite sprMusicOn, sprMusicOff, sprSFXOn, sprSFXOff;

    private void Awake()
    {
        GameUtils.EventHandlerIni();
        btnMusic.onClick.AddListener(ActionMusic);
        btnSFX.onClick.AddListener(ActionSFX);
        btnQuit.onClick.AddListener(() =>
        {
            PanelConfirmController.Instance.Init("WARNING", " Are you sure?", "YES", "NO", () =>
            {
                PanelLoading.LoadScene(GameConstant.GameScene.LOGIN);
            }, () =>
            {

            });
            PanelConfirmController.Instance.Show();
        });
        for (int i = 0; i < btnSelects.Length; i++)
        {
            int index = i;
            btnSelects[i].onClick.AddListener(() =>
            {

                SelectPanel(index);
            });
        }
        LoadMusicUI();
        SelectPanel(0);
    }
    private void OnEnable()
    {
        MusicManager.Instance.PlayMusic(MusicManager.Instance.MusicDB.Music_Home_Random);
    }

    private void OnDestroy()
    {
        GameUtils.EventHandlerReset();
    }

    public void SelectPanel(int index)
    {
        foreach (var item in panelSelects)
        {
            item.SetActive(false);
        }
        panelSelects[index].SetActive(true);
        foreach (var item in btnSelects)
        {
            item.image.sprite = sprNormal;
            // item.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        }
        btnSelects[index].image.sprite = sprSelect;
        //  btnSelects[index].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;

    }

    void LoadMusicUI()
    {
        imgMusic.sprite = (MusicManager.Instance.MusicVolume != 0) ? sprMusicOn : sprMusicOff;
        imgSFX.sprite = (MusicManager.Instance.SoundVolume != 0) ? sprSFXOn : sprSFXOff;
    }

    public void ActionMusic()
    {
        if (MusicManager.Instance.MusicVolume == 0)
        {
            MusicManager.Instance.MusicVolume = 1;
        }
        else
        {
            MusicManager.Instance.MusicVolume = 0;
        }
        LoadMusicUI();
    }
    public void ActionSFX()
    {
        if (MusicManager.Instance.SoundVolume == 0)
        {
            MusicManager.Instance.SoundVolume = 1;
        }
        else
        {
            MusicManager.Instance.SoundVolume = 0;
        }
        LoadMusicUI();
    }

}
