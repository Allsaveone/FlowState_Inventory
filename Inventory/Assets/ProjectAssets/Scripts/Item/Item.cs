using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public enum ItemType
{
    Junk,
    Consumable,
    Weapon,
    Armour,
    Head,
    Trinket
}

[Serializable]
public class Item
{
    //Make these get/set later for safety!
    public int ID;//{ get; set; }
    public string Name;// { get; set; }
    public Sprite Icon;// { get; set; }
    public string Description;// { get; set; }
    public ItemType ItemType; //{ get;  set; }
    public bool Stackable;// { get; set; }
    public int Value;// { get; set; }

    public Item(Item_Data data)
    {
        this.ID = data.id;
        this.Name = data.name;
        this.Icon = Resources.Load<Sprite>("Sprites/ItemIcons/Icons/128px/" + data.iconName); //NB!!! Need to Atlas these
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
