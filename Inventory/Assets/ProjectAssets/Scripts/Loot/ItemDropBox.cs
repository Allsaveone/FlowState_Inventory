/// <summary>
/// This is Loot dispay contoller - lootable items will display contents here.
/// </summary>
using System;
using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ItemDropBox : MonoBehaviour, IDropHandler  {

    public List<Item> testitems = new List<Item>();
    public DataManager data;// TESTING

    public bool storage;
    public GameObject lootSlot;
    public GameObject boxLayout;
    [Header("Items:")]
    public List<Item> items = new List<Item>();
    public Inventory inventory;

    private Item _droppedItem;
    private int ammount;

    void OnEnable()
    {
        TEST();

        if (items.Count == 0 && !storage)
        {
            //TEST();
            this.gameObject.SetActive(false);
            return;
        }
        else
        {
            if(items != null)
            DisplayItems(items);
        }
    }

    void TEST()
    {
        /*
        Item i1 = data.FindItemBy(data.item_DataBase.items, "Sword");
        testitems.Add(i1);
        Item i2 = data.FindItemBy(data.item_DataBase.items, "Bow");
        testitems.Add(i2);
        Item i3 = data.FindItemBy(data.item_DataBase.items, 5);
        testitems.Add(i3);
        */
        Item i4 = data.FindItemBy(data.item_DataBase.items, 7);
        testitems.Add(i4);

        DisplayItems(testitems);
    }

    public void DisplayItems(List<Item> newItems)
    {
        this.gameObject.SetActive(true);
        DisableDropBox();
        items = newItems;
        UpdateBox();
    }

    public void UpdateBox()
    {
        for (int i = 0; i < items.Count; i++)
        {
            UpdateBoxUI(items[i]);
        }
        //boxLayout.SetActive(true);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (storage)
        {
            InventorySlot_Helper draged_slotHelper = eventData.pointerDrag.GetComponentInChildren<InventorySlot_Helper>();//being dragged

            if (draged_slotHelper != null)
            {
                _droppedItem = draged_slotHelper.item;
                ammount = draged_slotHelper.amount;
                Invoke("DropItem", 0.0f);
                return;
            }
        }
       
    }

    public void DropItem()
    {
        if(_droppedItem != null || _droppedItem.ID != -1)
        {
            if (_droppedItem.Stackable)
            {
                for (int i = 0; i < ammount; i++)
                {
                    inventory.RemoveItem(inventory.items, _droppedItem);
                    items.Add(_droppedItem);
                    UpdateBoxUI(_droppedItem);
                }
            }
            else
            {
                inventory.RemoveItem(inventory.items, _droppedItem);
                items.Add(_droppedItem);
                UpdateBoxUI(_droppedItem);
            }
            //Spawn world object -DroppedItem Class
        }
    }

    public void UpdateBoxUI(Item newItem)
    {
        if (newItem.Stackable)
        {
            int count = 0;

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Stackable && items[i].ID == newItem.ID)
                {
                    count++;//checkfor max stack size when i get around to that...
                }
            }

            if (count <= 1)
            {
                GameObject go = Instantiate(lootSlot, transform.position, Quaternion.identity) as GameObject;
                LootSlot_Helper slotHelper = go.GetComponent<LootSlot_Helper>();
                slotHelper.SetUp(newItem,this);
                go.transform.SetParent(boxLayout.transform, false);
                slotHelper.SetAmmountText(1);

            }
            else
            {
                GameObject stack = boxLayout.transform.Find(newItem.Name).gameObject;
                LootSlot_Helper slotHelper = stack.GetComponent<LootSlot_Helper>();
                if(slotHelper.amount != count)
                slotHelper.SetAmmountText(count);
            }

        }
        else
        {
            GameObject go = Instantiate(lootSlot, transform.position, Quaternion.identity) as GameObject;
            LootSlot_Helper slotHelper = go.GetComponent<LootSlot_Helper>();
            slotHelper.SetUp(newItem,this);
            go.transform.SetParent(boxLayout.transform,false);
        }
    }

    public void DisableDropBox()
    {
        //boxLayout.SetActive(false);
        CleanUp();
    }

    public void CleanUp()
    {
        if(boxLayout.transform.childCount != 0)
        {
            foreach (Transform child in boxLayout.transform)
            {
                Destroy(child.gameObject);
            }
        }
        
    }

    public void ItemRemoved()
    {
        //Sfx!
        if (items.Count == 0)
        {
            DisableDropBox();
            this.gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        //items = null;
        CleanUp();
    }

}
