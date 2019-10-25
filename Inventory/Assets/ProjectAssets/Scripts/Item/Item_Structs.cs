using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

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
    public string itemJson;
}