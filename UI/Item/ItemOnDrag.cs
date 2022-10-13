using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOnDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Transform originaParent;
    public Inventory mybag;
    int currentItemID;

    public void OnBeginDrag(PointerEventData eventData)
    {
        originaParent = transform.parent;
        currentItemID = originaParent.GetComponent<Slot>().SlotID;
        transform.SetParent(transform.parent.parent);        
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        //Debug.LogError(eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.name == "Button")
        {
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent); // setparent至button的父集Item的父集Slot
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;//position至Slot
            var temp = mybag.itemList[currentItemID];
            mybag.itemList[currentItemID] = mybag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().SlotID];//交换mybag中物品List的位置
            mybag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().SlotID] = temp;        
            eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originaParent.position;
            eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originaParent);
            
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            return;
        }
        transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
        transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().SlotID != currentItemID)
        {
            mybag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().SlotID] = mybag.itemList[currentItemID];
            mybag.itemList[currentItemID] = null;
        }
    }
}
