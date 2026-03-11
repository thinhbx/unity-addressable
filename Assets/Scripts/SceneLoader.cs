using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    AsyncOperationHandle<SceneInstance> handle;

    public async void LoadScene(string sceneKey)
    {
        handle = Addressables.LoadSceneAsync(sceneKey);
        await handle.Task;
    }

    public void Unload()
    {
        Addressables.UnloadSceneAsync(handle);
    }
}