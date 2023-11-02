using DG.Tweening.Plugins.Core.PathCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

public class ResourceManager
{

    Dictionary<string, UnityEngine.Object> _resources = new Dictionary<string, Object>();

    public T Load<T>(string key) where T : Object
    {
        if (_resources.TryGetValue(key, out Object resource))
        {
            return resource as T;
        }

        if (typeof(T) == typeof(Sprite))
        {
            key = key + ".sprite";
            if (_resources.TryGetValue(key, out Object temp))
            {
                return temp as T;
            }
        }

        return null;
    }

    public GameObject Instantiate(string key, Transform parent = null, bool pooling = false)
    {
        GameObject prefab = Load<GameObject>($"{key}");
        if (prefab == null)
        {
            Debug.LogError($"Failed to load prefab : {key}");
            return null;
        }

        if (pooling)
            return Managers.Pool.Pop(prefab);

        GameObject go = Object.Instantiate(prefab, parent);

        go.name = prefab.name;
        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null) return;
        if (Managers.Pool.Push(go)) return;

        Object.Destroy(go);
    }

    public void LoadAsync<T>(string Key, Action<T> callback = null) where T : UnityEngine.Object
    {
        //스프라이트인 경우 하위객체의 이름으로 로드하면 스프라이트로 로딩이 됨 
        string loadKey = Key;
        if (Key.Contains(".sprite"))
            loadKey = $"{Key}[{Key.Replace(".sprite", "")}]";

        var asyncOperation = Addressables.LoadAssetAsync<T>(loadKey);

        asyncOperation.Completed += (op) =>
        {
            //캐시확인 
            if (_resources.TryGetValue(Key, out Object resource))
            {
                callback?.Invoke(op.Result);
                return;
            }

            _resources.Add(Key, op.Result);
            Debug.Log(_resources[Key] + " : " + _resources[Key].GetType());
            callback?.Invoke(op.Result);
        };
    }

    public void LoadAllAsync<T>(string label, Action<string, int, int> callback) where T : UnityEngine.Object
    {
        var OpHandle = Addressables.LoadResourceLocationsAsync(label, typeof(T));

        OpHandle.Completed += (op) =>
        {
            int loadCount = 0;

            int totalCount = op.Result.Count;

            foreach (var result in op.Result)
            {
                if (result.PrimaryKey.Contains(".sprite"))
                {
                    LoadAsync<Sprite>(result.PrimaryKey, (obj) =>
                    {
                        loadCount++;
                        callback?.Invoke(result.PrimaryKey, loadCount, totalCount);
                    });
                }
                else
                {
                    LoadAsync<T>(result.PrimaryKey, (obj) =>
                    {
                        loadCount++;
                        callback?.Invoke(result.PrimaryKey, loadCount, totalCount);
                    });
                }
            }
        };
    }
}
