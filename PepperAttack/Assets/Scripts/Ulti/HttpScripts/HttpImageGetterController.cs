using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// ImageGetter.GetImage("https://cdn.tgdd.vn/GameApp/3/221113/Screentshots/hero-wars-fantasy-world-the-gioi-mong-ao-01-06-2021-0.png", (s) =>{i_2.sprite = s;});
/// </summary>
public class HttpImageGetterController
{
    string SAVE_PATH;
    MonoBehaviour MONO;
    public HttpImageGetterController(MonoBehaviour mono)
    {
        this.MONO = mono;
        SAVE_PATH = Path.Combine(Application.persistentDataPath, "cache");
        //  SAVE_PATH = Path.Combine(SAVE_PATH, "Images");
    }

    public void GetImage(string url, Action<Sprite> OnLoadDone)
    {
        if (!CheckImageAndLoad(url, OnLoadDone))
        {
            downloadImage(url, OnLoadDone);
        }
        else
        {
            Debug.Log("Get Image Done from cache " + url);
        }
    }
    bool CheckImageAndLoad(string url, Action<Sprite> OnLoadDone)
    {
        //string path = Path.Combine(SAVE_PATH, url.GetFileNameFromUrl());
        //byte[] imageBytes = loadImage(path);
        //Texture2D texture = new Texture2D(2, 2);
        //if (imageBytes != null)
        //{
        //    texture.LoadImage(imageBytes);
        //    Sprite mySprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        //    if (OnLoadDone != null)
        //        OnLoadDone(mySprite);
        //    return true;
        //}
        //else
        //{
        return false;
        //}
    }
    private void downloadImage(string url, Action<Sprite> OnLoadDone)
    {
        WWW www = new WWW(url);
        //  string path = Path.Combine(SAVE_PATH, url.GetFileNameFromUr        //MONO.StartCoroutine(_downloadImage(www, path, (isDone) =>
        //{
        //    if (isDone)
        //        CheckImageAndLoad(url, OnLoadDone);
        //    else
        //       if (OnLoadDone != null)
        //        OnLoadDone(null);

        //}));l());

    }

    private IEnumerator _downloadImage(WWW www, string savePath, Action<bool> OnDone)
    {
        yield return www;

        //Check if we failed to send
        if (string.IsNullOrEmpty(www.error))
        {
            //  UnityEngine.Debug.Log("Success");

            //Save Image
            saveImage(savePath, www.bytes, OnDone);
        }
        else
        {
            UnityEngine.Debug.LogError("Error: " + www.error);
        }
    }

    void saveImage(string path, byte[] imageBytes, Action<bool> OnDone)
    {
        //Create Directory if it does not exist
        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }

        try
        {
            File.WriteAllBytes(path, imageBytes);
            //Debug.Log("Saved Data to: " + path.Replace("/", "\\"));
            if (OnDone != null) OnDone(true);
        }
        catch (Exception e)
        {
            // Debug.LogWarning("Failed To Save Data to: " + path.Replace("/", "\\"));
            Debug.LogError("Error: " + e.Message);
            if (OnDone != null) OnDone(false);
        }
    }

    byte[] loadImage(string path)
    {
        byte[] dataByte = null;

        //Exit if Directory or File does not exist
        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Debug.LogWarning("Directory does not exist");
            return null;
        }

        if (!File.Exists(path))
        {
            Debug.Log("File does not exist");
            return null;
        }

        try
        {
            dataByte = File.ReadAllBytes(path);
            //  Debug.Log("Loaded Data from: " + path.Replace("/", "\\"));
        }
        catch (Exception e)
        {
            //Debug.LogWarning("Failed To Load Data from: " + path.Replace("/", "\\"));
            Debug.LogError("Error: " + e.Message);
        }

        return dataByte;
    }
}
