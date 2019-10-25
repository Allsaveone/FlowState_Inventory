using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    [Header("Inventory:")]
    public int numOfSlots = 16;
    public int numOfSlotUsed = 0;
    public bool inventoryFull = false;
    public List<Item> items = new List<Item>();
    [HideInInspector]
    public List<GameObject> inventorySlots;

    [Header("Ui Elements:")]
    public GameObject inventoryPanel;
    public Transform inventoryLayOut;
    public GameObject inventorySlot_Prefab;
    public ToolTip toolTip;
    public InventorySlot_Helper draggedSlot;

    [Header("Testing:")]
    public bool addTestItems = false;
    public DataManager data;// TESTING

    void Start ()
    {
        inventoryPanel.SetActive(false);
        Invoke("LateStart", 1f);// Placeholder, waits for data to load
    }

    void LateStart()
    {
        //inventoryPanel.SetActive(true);
        Create_InventorySlots(numOfSlots);
    }

    void Clear_InventorySlots()
    {
        foreach (Transform uiSlot in inventoryLayOut)
        {
            Destroy(uiSlot.gameObject);
        }
    }

    void Create_InventorySlots (int inventorySize)
    {
        Clear_InventorySlots();

        for (int i = 0; i < inventorySize; i++)
        {
            GameObject go = Instantiate(inventorySlot_Prefab, inventoryLayOut.transform.position, Quaternion.identity) as GameObject;
            go.transform.SetParent(inventoryLayOut,false);

            InventorySlot_Helper helper = go.GetComponentInChildren<InventorySlot_Helper>();
            
            helper.slotId = i;
            helper.slotHolder = go;
            helper.inventory = this;
            //Debug.Log(helper.slotHolder.name);

            helper.holderParent = GameObject.Find("GameCanvas");//inventoryLayOut.transform.parent.gameObject;

            Item blankItem = new Item();
            items.Add(blankItem);//fill with null items.
            helper.item = blankItem;

            //slots[i].name = itemToAdd.Name;
            InventorySlot slot = go.GetComponent<InventorySlot>();
            slot.slotId = helper.slotId;
            slot.inventory = this;

            inventorySlots.Add(go);
        }

        if (addTestItems)
        {
            Add_Item( data.FindItemBy(data.item_DataBase.items, 1));
            Add_Item( data.FindItemBy(data.item_DataBase.items, 1));
            Add_Item( data.FindItemBy(data.item_DataBase.items, 1));
            Add_Item( data.FindItemBy(data.item_DataBase.items, 1));
            Add_Item( data.FindItemBy(data.item_DataBase.items, 2));
            Add_Item( data.FindItemBy(data.item_DataBase.items, 2));
            Add_Item( data.FindItemBy(data.item_DataBase.items, "Sword"));
            Add_Item( data.FindItemBy(data.item_DataBase.items, "Bow"));
            Add_Item(data.FindItemBy(data.item_DataBase.items, "Armour"));
            Add_Item(data.FindItemBy(data.item_DataBase.items, "Medal"));
            Add_Item(data.FindItemBy(data.item_DataBase.items, "Helm"));
        }
        //Load Saved invetory Data
        //inventoryPanel.SetActive(true);
    }

    public void Add_Item( Item itemToAdd)
    {
        if(numOfSlotUsed >= numOfSlots)
        {
            Debug.LogWarning("Inventory Full");
            inventoryFull = true;
            return;
        }

        if (itemToAdd.Stackable && CheckForItemStacks(itemToAdd))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == itemToAdd.ID)
                {
                    InventorySlot_Helper helper = inventorySlots[i].GetComponentInChildren<InventorySlot_Helper>();
                    helper.amount++;
                    helper.itemAmmountText.text = helper.amount.ToString();
                    helper.itemAmmountText.gameObject.SetActive(true);
                    //Debug.Log("Stacked " + items[i].Name);
                    break;
                }
            }
        }
        else
        {
            //Debug.Log("NonStacking- " + itemToAdd.Name);

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Name == "")
                {
                    items[i] = itemToAdd;
                    InventorySlot_Helper helper = inventorySlots[i].GetComponentInChildren<InventorySlot_Helper>();
                    helper.item = items[i];
                    helper.amount = 1;
                    helper.slotId = i;
                    helper.slotHolder = helper.gameObject;
                    helper.holderParent = GameObject.Find("GameCanvas");//inventoryLayOut.transform.parent.gameObject;
                    helper.inventory = this;
                    helper.itemSpriteImage.sprite = itemToAdd.Icon;
                    helper.itemSpriteImage.enabled = true;
                    helper.itemSpriteImage.gameObject.SetActive(true);

                    //slots[i].name = itemToAdd.Name;
                    InventorySlot slot = inventorySlots[i].GetComponent<InventorySlot>();
                    slot.slotId = helper.slotId;
                    slot.inventory = this;

                    helper.gameObject.name = itemToAdd.Name;
                    helper.slotHolder = slot.gameObject;
                    //Debug.Log("New " + items[i].Name);

                    numOfSlotUsed++;

                    break;
                }                 
            }
        }

        if (numOfSlotUsed >= numOfSlots)
        {
            Debug.LogWarning("Inventory Full");
            inventoryFull = true;
            return;
        }
        else if (numOfSlotUsed <= numOfSlots)
        {
            //Debug.Log("Inventory has " + (numOfSlots - numOfSlotUsed).ToString() + " slots available");
        }
    }

    public void RemoveItem(List<Item> itemList, Item itemToRemove)
    {
        //Debug.LogWarning("Try RemoveItem " + itemToRemove.Name);

        if (itemList.Contains(itemToRemove))
        {
            Debug.LogWarning("Inventory contains " + itemToRemove.Name);

            if (itemToRemove.Stackable && CheckForItemStacks(itemToRemove))//Stackable items
            {
                int i = itemList.IndexOf(itemToRemove);

                InventorySlot_Helper helper = inventorySlots[i].GetComponentInChildren<InventorySlot_Helper>();

                if (helper == null)
                    Debug.LogError("WTF?");

                if (helper.amount >= 2)//reduce Stack of items
                {
                    //Debug.Log("Removed one " + itemToRemove.Name + " from a Stack");
                    helper.amount--;
                    helper.itemAmmountText.text = helper.amount.ToString();

                    if (helper.amount == 1)
                    {
                        helper.itemAmmountText.gameObject.SetActive(false);
                    }
                }
                else//last item in Stack (1 item)
                {
                    //Debug.Log(itemToRemove.Name + " Stack finished");
                    int s = itemList.IndexOf(itemToRemove);
                    itemList[s] = new Item();
                    InventorySlot_Helper lastInStack_helper = inventorySlots[s].GetComponentInChildren<InventorySlot_Helper>();
                    lastInStack_helper.item = itemList[s];
                    lastInStack_helper.itemSpriteImage.enabled = false;
                    lastInStack_helper.gameObject.name = "InventoryItem";
                    numOfSlotUsed--;
                    //Debug.LogWarning(items[s].Name);
                }
            }
            else//non Stackable items
            {
                //Debug.Log(itemToRemove.Name + " is not Stackable");
                int i = itemList.IndexOf(itemToRemove);
                itemList[i] = new Item();
                InventorySlot_Helper helper = inventorySlots[i].GetComponentInChildren<InventorySlot_Helper>();
                helper.item = itemList[i];
                helper.itemSpriteImage.enabled = false;
                helper.gameObject.name = "InventoryItem";
                numOfSlotUsed--;
                //Debug.LogWarning(items[i].Name);
            }
        }

        if (numOfSlotUsed < numOfSlots)
        {
            //Debug.Log("Inventory has " + (numOfSlots - numOfSlotUsed).ToString() + " slots available");
            inventoryFull = false;
        }
    }

    public void RemoveItemBy(List<Item> itemList, int iD)
    {
        for (int c = 0; c < itemList.Count; c++)
        {
            if (itemList[c].ID == iD)
            {
                Item itemtoRemove = itemList[c];
                RemoveItem(itemList, itemtoRemove);
                break;
            }
        }
    }

    public void RemoveItemBy(List<Item> itemList, string itemName)
    {
        for (int c = 0; c < itemList.Count; c++)
        {
            if (itemList[c].Name == itemName)
            {
                Item itemtoRemove = itemList[c];
                RemoveItem(itemList, itemtoRemove);
                break;
            }
        }
    }

    bool CheckForItemStacks(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == item.ID)
            {
                return true;
            }
        }
        return false;
    }
    
    #region Testing Methods
    public void TEST_RemoveItem()
    {
        RemoveItemBy(items,3);
    }

    public void TEST_AddItem()//Add item from Db by its id
    {
        Add_Item( data.FindItemBy(data.item_DataBase.items, 3));
    }
    #endregion
}


