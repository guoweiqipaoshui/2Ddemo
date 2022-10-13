using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    public Item thisItem;
    public Inventory PlayerBag;
    private void OnTriggerEnter2D(Collider2D worldItem)
    {
        if(worldItem.gameObject.CompareTag("Player"))
        {
            AddItem();
            Destroy(gameObject);       
        }
    }

    public void AddItem()
    {
        if(!PlayerBag.itemList.Contains(thisItem))
        {
            //PlayerBag.itemList.Add(thisItem);
            //InventoryManager.NewItem(thisItem);
            for (int i = 0; i < PlayerBag.itemList.Count; i++)
            {
                if(PlayerBag.itemList[i] == null)
                {
                    PlayerBag.itemList[i] = thisItem;
                    break;
                }
            }

        }else
        {
            thisItem.itemNum += 1;
        }

        InventoryManager.RefreshItem();
    }
}
