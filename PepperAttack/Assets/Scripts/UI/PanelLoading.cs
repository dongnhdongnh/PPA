using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelLoading : MonoBehaviour
{
    [SerializeField]
    Image img_process;

    public static string nextSceneName;

    AsyncOperation _operation;
    public static void LoadScene(string name)
    {
        nextSceneName = name;
        SceneManager.LoadScene(GameConstant.GameScene.LOADING);
    }
    // Start is called before the first frame update
    void Start()
    {
        _operation = SceneManager.LoadSceneAsync(nextSceneName);
        _operation.allowSceneActivation = false;
        StartCoroutine(IELoading());
    }


    IEnumerator IELoading()
    {
        while (true)
        {
            img_process.rectTransform.localScale = new Vector3(_operation.progress, 1, 1);
            yield return null;

            if (_operation.isDone || _operation.progress >= 1) break;
        }
        _operation.allowSceneActivation = true;

    }

}
