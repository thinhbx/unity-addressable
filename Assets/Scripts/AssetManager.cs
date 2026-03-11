using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AssetManager : MonoBehaviour
{
    private static Dictionary<string, AsyncOperationHandle> cache =
        new Dictionary<string, AsyncOperationHandle>();

    public static async Task<T> Load<T>(string key)
    {
        if (cache.ContainsKey(key))
        {
            return (T)cache[key].Result;
        }

        var handle = Addressables.LoadAssetAsync<T>(key);
        await handle.Task;

        cache[key] = handle;

        return handle.Result;
    }

    public static void Release(string key)
    {
        if (!cache.ContainsKey(key)) return;

        Addressables.Release(cache[key]);
        cache.Remove(key);
    }
}