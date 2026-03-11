using UnityEngine;

public class AddressableSpawner : MonoBehaviour
{
    public string enemyKey = "Enemy";

    async void Start()
    {
        var prefab = await AssetManager.Load<GameObject>(enemyKey);
        Instantiate(prefab, Vector3.zero, Quaternion.identity);
    }
}