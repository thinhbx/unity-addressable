using UnityEngine;
using UnityEngine.EventSystems;

public class DropItem : MonoBehaviour, IDropHandler
{
    public string targetID;
    public void OnDrop(PointerEventData eventData)
    {
        DragItem dragItem = eventData.pointerDrag.GetComponent<DragItem>();

        if (dragItem != null)
        {
            if(dragItem.id == targetID)
            {
                Debug.Log("Correct");
            }
            else
            {
                Debug.Log("WWrong");
            }
        }
    }


}
