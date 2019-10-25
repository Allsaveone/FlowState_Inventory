using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class Trinket_Item : Item {

    [SerializeField]
    public string trinketData;

    public Trinket_Item(Item_Data data)
    {
        ID = data.id;
        Name = data.name;
        //Icon = SpriteAtlasManager.Instance.GetSprite(data.iconName); //Resources.Load<Sprite>("Sprites/ItemIcons/Icons/128px/" + data.iconName);
        GetItemSprite(data.iconName);
        Description = data.description;
        ItemType = data.itemType;
        Stackable = data.stackable;
        Value = data.value;
        trinketData = data.itemJson;
    }


    public Trinket_Item()//For clearing out an items Datawhen unequiping
    {
        this.ID = -1;
        this.Name = "";
        this.Description = "";
        //Stats/ItemType
        this.Stackable = false;
        this.Value = -1;
        this.trinketData = null;
    }

    public void TrinketData()
    {

    }
}
