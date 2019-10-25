using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class DataManager : MonoBehaviour {

    public const string DATAPATH = "/StreamingAssets/Data/";
    public const string FILETYPE = ".Json";
    public ItemCollection_Data item_DataBase;

    public bool reInit_DB = false; // TEST CONTROL - Clears and reInits the DB

    void Start()//Need a better interface for resetting the Database-Placeholder
    {
        if (reInit_DB)
        {
            Init_DataBase("Item_DataBase");
        }
        else
        {
            RetrieveDataFiles("Item_DataBase");
        }
    }

    private void Init_DataBase(string dataBaseName)//adds test items to DB and writes out the JSON file
    {
        ItemCollection_Data newItemCollection = new ItemCollection_Data();

        Item_Data testIitemData = Create_Item(1, "Steak", "Inventory_Steak", "Steak.....Mmmmm", ItemType.Consumable,true,1);
        Item_Data testIitemData2 = Create_Item(2, "Silver Key", "Inventory_Key", "A shiny key", ItemType.Junk, false, 1);
        Item_Data testIitemData3 = Create_Item(3, "Sword", "Weapons_Sword", "An iron sword", ItemType.Weapon, false, 1,"{Strenght : 5}");
        Item_Data testIitemData4 = Create_Item(4, "Bow", "Weapons_Bow", "A short bow", ItemType.Weapon, false, 1);
        Item_Data testIitemData5 = Create_Item(5, "Armour", "Equipment_Vest", "Rusty armour", ItemType.Armour, false, 1, "{Armour : 5}");
        Item_Data testIitemData6 = Create_Item(6, "Medal", "Rewards_MedalStar", "Old war medal", ItemType.Trinket, false, 1);
        Item_Data testIitemData7 = Create_Item(7, "Helm", "Equipment_Helmet", "Rusty iron helmet", ItemType.Head, false, 1, "{Health : 5}");

        List<Item_Data> items = new List<Item_Data>();
        items.Add(testIitemData);
        items.Add(testIitemData2);
        items.Add(testIitemData3);
        items.Add(testIitemData4);
        items.Add(testIitemData5);
        items.Add(testIitemData6);
        items.Add(testIitemData7);

        newItemCollection.items = items;

        string jsonItemDB = JsonUtility.ToJson(newItemCollection);
        File.WriteAllText(Application.dataPath + DATAPATH + dataBaseName + FILETYPE, jsonItemDB);
        RetrieveDataFiles("Item_DataBase");
    }

    Item_Data Create_Item(int id, string name, string iconName,string description, ItemType type,bool stackable, int value) // Create item Method
    {
        Item_Data testItem = new Item_Data();

        testItem.id = id;
        testItem.name = name;
        testItem.iconName = iconName;
        testItem.description = description;
        testItem.itemType = type;
        //Stats/ItemType
        testItem.stackable = stackable;
        testItem.value = value;
        Debug.Log("Created-" + testItem.name);
        return testItem;
    }

    Item_Data Create_Item(int id, string name, string iconName, string description, ItemType type, bool stackable, int value,string json) // Create item Method
    {
        Item_Data testItem = new Item_Data();

        testItem.id = id;
        testItem.name = name;
        testItem.iconName = iconName;
        testItem.description = description;
        testItem.itemType = type;
        //Stats/ItemType
        testItem.stackable = stackable;
        testItem.value = value;
        testItem.itemJson = json;
        Debug.Log("Created with Data-" + testItem.name);
        return testItem;
    }

    void RetrieveDataFiles(string fileName)
    {
        DirectoryInfo dB_DirectoryPath = new DirectoryInfo(Application.dataPath + DATAPATH);
        FileInfo[] dataBaseFiles = dB_DirectoryPath.GetFiles("*.*");
        
        foreach (FileInfo file in dataBaseFiles)
        {
            if (file.Extension == FILETYPE)
            {
                if(file.Name == fileName + FILETYPE)
                {
                    if (fileName == "Item_DataBase")//Make switch case method later
                    {
                        FileInfo itemDataBaseFile;
                        itemDataBaseFile = file;

                        Construct_ItemDataBase(itemDataBaseFile);
                    }
                }
            }
        }
    }

    void Construct_ItemDataBase(FileInfo sourceFile)
    {
        string loadedItemData = File.ReadAllText(sourceFile.ToString());
        item_DataBase = JsonUtility.FromJson<ItemCollection_Data>(loadedItemData);

        //string test = JsonUtility.ToJson(item_DataBase);
        //Debug.Log(test);
        #region //TestSearch
        /*
        //Test search
        Item temp = FindItemByID(item_DataBase.items, 1);
        if (temp != null)
        {
            Debug.Log(temp.ID + temp.Name);
        }
        else
        {
            Debug.Log("Item Not Found");
        }
        */
        #endregion
    }

    public Item FindItemBy(List<Item_Data> itemList,int iD)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            
            if (itemList[i].id == iD)
            {
                return ReturnItemByType(itemList, i);
            }
        }
        return null;
    }

    public Item FindItemBy(List<Item_Data> itemList, string itemName)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            
            if (itemList[i].name == itemName)
            {

                return ReturnItemByType(itemList, i);
/*
                if (itemList[i].itemType == ItemType.Weapon)
                {
                    //Debug.Log("WeaponItem");
                    

                    return ReturnItemByType(itemList, i);

                    //return new WeaponItem(itemList[i]);
                }
                else
                {
                    return new Item(itemList[i]);

                }
*/
            }
        }
        return null;
    }

    Item ReturnItemByType(List<Item_Data> itemList,int index)
    {
        switch (itemList[index].itemType)
        {
            case ItemType.Weapon:
                return new WeaponItem(itemList[index]);
            case ItemType.Head:
                return new Head_Item(itemList[index]);
            case ItemType.Armour:
                return new Armour_Item(itemList[index]);
            case ItemType.Trinket:
                return new Trinket_Item(itemList[index]);
            case ItemType.Consumable:
                return new Consumable_Item(itemList[index]);
            default:
                break;
        }

        return new Item(itemList[index]);
    }
}

/*
[Serializable]
public struct ItemCollection_Data
{
    public List<Item_Data> items;
}

[Serializable]
public struct Item_Data
{
    public int id;
    public string name;
    public string iconName;
    public string description;
    public ItemType itemType;
    //Stats/ItemType
    public bool stackable;
    public int value;
}

[Serializable]
public enum ItemType
{
    Junk,
    Consumable,
    Weapon,
    Armour,
    Trinket
}

//[Serializable]
public class Item 
{
    public int ID { get; set; }
    public string Name { get; set; }
    public Sprite Icon { get; set; }
    public string Description { get; set; }
    public ItemType ItemType { get; set; }
    public bool Stackable { get; set; }
    public int Value { get; set; }

    public Item(Item_Data data)
    {
        this.ID = data.id;
        this.Name = data.name;
        this.Icon = Resources.Load<Sprite>("Sprites/ItemIcons/Icons/128px/" + data.iconName);
        this.Description = data.description;
        this.ItemType = data.itemType;
        this.Stackable = data.stackable;
        this.Value = data.value;
    }

    public Item()//For clearing out an items Data
    {
        this.ID = -1;
        this.Name = "";
        this.Description = "";
        //Stats/ItemType
        this.Stackable = false;
        this.Value = -1;
    }
}

public class WeaponItem : Item {

    public WeaponItem(Item_Data data)
    {
        this.ID = data.id;
        this.Name = data.name;
        this.Icon = Resources.Load<Sprite>("Sprites/ItemIcons/Icons/128px/" + data.iconName);
        this.Description = data.description;
        this.ItemType = data.itemType;
        this.Stackable = data.stackable;
        this.Value = data.value;
    }
}

*/
