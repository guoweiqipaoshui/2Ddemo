using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;

    public Inventory mybag;
    public GameObject slotGrid;
    //public Slot slotPrefab;
    public GameObject emptySlot;
    public Text itemInfo;

    public List<GameObject> itemList;

    private void OnEnable()
    {
        RefreshItem();
        instance.itemInfo.text = "";
    }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }

    public static void UpdateItemInfo(string Info)
    {
        instance.itemInfo.text = Info;
    }

    /*public static void NewItem(Item item)
    {
        Slot newItem = Instantiate(instance.slotPrefab,instance.slotGrid.transform.position,Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform,false);
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.itemImage;
        newItem.slotNum.text = item.itemNum.ToString();
    }*/

    public static void RefreshItem()
    {
        for(int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if (instance.slotGrid.transform.childCount == 0)
                break;
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
            instance.itemList.Clear();
        }

        for(int i = 0; i < instance.mybag.itemList.Count; i++)
        {
            //NewItem(instance.mybag.itemList[i]);
            instance.itemList.Add(Instantiate(instance.emptySlot));
            instance.itemList[i].transform.SetParent(instance.slotGrid.transform,false);
            instance.itemList[i].GetComponent<Slot>().SlotID = i;
            instance.itemList[i].GetComponent<Slot>().SetUp(instance.mybag.itemList[i]);
        }
    }
}
