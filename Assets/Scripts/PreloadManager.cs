using UnityEngine;
using UnityEngine.AddressableAssets;

public class PreloadManager : MonoBehaviour
{
    public string preloadLabel = "Characters";

    async void Start()
    {
        var handle = Addressables.DownloadDependenciesAsync(preloadLabel);
        await handle.Task;
        Debug.Log("Preload Complete");
    }
}