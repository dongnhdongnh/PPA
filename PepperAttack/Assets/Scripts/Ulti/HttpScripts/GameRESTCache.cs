using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GameRESTCacheKey<T1, T2>
{
    public readonly T1 Item1;
    public readonly T2 Item2;
    public GameRESTCacheKey(T1 item1, T2 item2) { Item1 = item1; Item2 = item2; }
}
public class GameRESTCache
{
    Dictionary<string, Dictionary<GameRESTCacheKey<string, string>, HttpREsultObject>> caches;
    public enum TAG
    {
        MISSION
    }
    public GameRESTCache()
    {
        caches = new Dictionary<string, Dictionary<GameRESTCacheKey<string, string>, HttpREsultObject>>();
    }
    string KEY = "GameRESTCache_{0}";
    public void SetCache(TAG tag, string req, string data, HttpREsultObject res)
    {
        string key = string.Format(KEY, tag.ToString());
        if (!caches.ContainsKey(key))
            caches.Add(key, new Dictionary<GameRESTCacheKey<string, string>, HttpREsultObject>());
        Dictionary<GameRESTCacheKey<string, string>, HttpREsultObject> cache_per_cache = caches[key];
        GameRESTCacheKey<string, string> _key = new GameRESTCacheKey<string, string>(req, data);
        if (!cache_per_cache.ContainsKey(_key))
        {
            cache_per_cache.Add(_key, res);
        }

    }
    public bool GetCache(TAG tag, string req,string data, out HttpREsultObject res)
    {
        string key = string.Format(KEY, tag.ToString());
        if (!caches.ContainsKey(key))
        {
            res = null;
            return false;
        }
        Dictionary<GameRESTCacheKey<string, string>, HttpREsultObject> cache_per_cache = caches[key];
        GameRESTCacheKey<string, string> _key = new GameRESTCacheKey<string, string>(req, data);
        if (cache_per_cache.ContainsKey(_key))
        {
            res = cache_per_cache[_key]; return true;
        }
        res = null;
        return false;
    }


}
