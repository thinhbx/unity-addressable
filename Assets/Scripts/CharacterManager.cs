using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets; // Thư viện chính
using UnityEngine.ResourceManagement.AsyncOperations; // Để quản lý tiến trình tải

public class CharacterManager : MonoBehaviour
{
    // [AssetReferenceUILabelRestriction] sẽ lọc Inspector 
    // Chỉ những Asset nào có nhãn "Hero" mới hiện ra
    [AssetReferenceUILabelRestriction("Hero")]
    public AssetReference heroReference;

    private GameObject spawnedHero;

    void Update()
    {
        // Nhấn Phím Space để triệu hồi Hero đã chọn
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnCharacter();
        }

        // Nhấn Phím R để xóa (Release) Hero khỏi bộ nhớ
        if (Input.GetKeyDown(KeyCode.R))
        {
            DespawnCharacter();
        }
    }

    void SpawnCharacter()
    {
        if (spawnedHero != null) return;

        AsyncOperationHandle<GameObject> handle = heroReference.InstantiateAsync();

        handle.Completed += (op) => {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                spawnedHero = op.Result;
                Debug.Log("Hero đã được triệu hồi thành công!");
            }
        };
    }

    void DespawnCharacter()
    {
        if (spawnedHero != null)
        {
            // CỰC KỲ QUAN TRỌNG: Dùng Addressables.ReleaseInstance để giải phóng RAM
            Addressables.ReleaseInstance(spawnedHero);
            spawnedHero = null;
            Debug.Log("Đã giải phóng bộ nhớ cho Hero");
        }
    }
}