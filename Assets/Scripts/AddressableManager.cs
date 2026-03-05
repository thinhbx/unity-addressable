using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddressableManager : MonoBehaviour
{
    [SerializeField] private AssetReference playerPrefabAssetReference;
    [SerializeField] private AssetReferenceTexture2D logoAssetReference;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private RawImage uwtLogo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Addressables.InitializeAsync().Completed += AddressableManager_Completed;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void AddressableManager_Completed(AsyncOperationHandle<IResourceLocator> handle)
    {
        LoadAddressableLevel("Assets/StarterAssets/ThirdPersonController/Scenes/Env 01.unity");
        playerPrefabAssetReference.InstantiateAsync().Completed += (go) =>
        {
            var player = go.Result; ;
            virtualCamera.Follow = player.transform.Find("PLayerCameraRoot");
        };
        logoAssetReference.LoadAssetAsync<Texture2D>().Completed += ShowLogo;

    }

    private void ShowLogo(AsyncOperationHandle<Texture2D> handle)
    {
        if (logoAssetReference.Asset != null)
        {
            uwtLogo.texture = logoAssetReference.Asset as Texture2D;
        }
    }

    public void LoadAddressableLevel(string addressableKey)
    {
        Addressables.LoadSceneAsync(addressableKey, LoadSceneMode.Additive).Completed += (asyncHandle) =>
        {
            Debug.Log("Load Scene Successfully!");
        };
    }

}
