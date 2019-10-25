using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class Head_Item : Item {

    [SerializeField]
    public string headData;

    public Head_Item(Item_Data data)
    {
        ID = data.id;
        Name = data.name;
        Icon = Resources.Load<Sprite>("Sprites/ItemIcons/Icons/128px/" + data.iconName);
        Description = data.description;
        ItemType = data.itemType;
        Stackable = data.stackable;
        Value = data.value;
        headData = data.itemJson;
    }

    public Head_Item()//For clearing out an items Data when unequiping
    {
        this.ID = -1;
        this.Name = "";
        this.Description = "";
        //Stats/ItemType
        this.Stackable = false;
        this.Value = -1;
        this.headData = null;
    }

    public void HeadData()
    {

    }
}
