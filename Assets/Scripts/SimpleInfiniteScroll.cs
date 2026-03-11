using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SimpleInfiniteScroll : MonoBehaviour
{
    [Header("Settings")]
    public ScrollRect scrollRect;
    public RectTransform itemPrefab;
    public int totalItemsData = 100; // Tổng số lượng dữ liệu (ví dụ 100 người bạn)

    [Header("Pool Config")]
    public int poolSize = 7; // Số lượng Object hiển thị thực tế
    public float itemHeight = 100f;
    public float spacing = 10f;

    private List<RectTransform> _instantiatedItems = new List<RectTransform>();
    private float _threshold; // Ngưỡng để bắt đầu dịch chuyển item

    void Start()
    {
        // 1. Đặt kích thước cho Content để Scrollbar hoạt động đúng
        float totalContentHeight = totalItemsData * (itemHeight + spacing);
        scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, totalContentHeight);

        // 2. Tạo Pool ban đầu
        for (int i = 0; i < poolSize; i++)
        {
            RectTransform item = Instantiate(itemPrefab, scrollRect.content);
            item.anchoredPosition = new Vector2(0, -i * (itemHeight + spacing));
            _instantiatedItems.Add(item);
            UpdateItemData(item, i); // Cập nhật dữ liệu ban đầu
        }

        // 3. Lắng nghe sự kiện cuộn
        _threshold = (itemHeight + spacing) * 1.5f;
        scrollRect.onValueChanged.AddListener(OnScroll);
    }

    void OnScroll(Vector2 scrollPos)
    {
        foreach (var item in _instantiatedItems)
        {
            // Tính toán khoảng cách từ Item đến Viewport
            float distance = item.anchoredPosition.y + scrollRect.content.anchoredPosition.y;

            // Nếu item trôi lên quá xa khỏi màn hình (Top) -> Đẩy xuống dưới
            if (distance > _threshold)
            {
                MoveItem(item, -(poolSize * (itemHeight + spacing)));
            }
            // Nếu item trôi xuống quá xa khỏi màn hình (Bottom) -> Đẩy lên trên
            else if (distance < -(_threshold + (poolSize - 1) * (itemHeight + spacing)))
            {
                MoveItem(item, (poolSize * (itemHeight + spacing)));
            }
        }
    }

    void MoveItem(RectTransform item, float offset)
    {
        Vector2 newPos = item.anchoredPosition;
        newPos.y += offset;

        // Giới hạn không cho dịch chuyển ra ngoài phạm vi dữ liệu thực tế
        if (newPos.y > 0 || newPos.y < -(totalItemsData * (itemHeight + spacing))) return;

        item.anchoredPosition = newPos;

        // Tính toán Index dữ liệu dựa trên vị trí mới
        int dataIndex = Mathf.Abs(Mathf.RoundToInt(newPos.y / (itemHeight + spacing)));
        UpdateItemData(item, dataIndex);
    }

    void UpdateItemData(RectTransform item, int index)
    {
        // Thịnh thay đổi Text hoặc Image của Item tại đây dựa trên Index
        item.GetComponentInChildren<Text>().text = "Người dùng số: " + index;
    }
}