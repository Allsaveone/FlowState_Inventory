using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class Armour_Item : Item {

    [SerializeField]
    public string armourData;

    public Armour_Item(Item_Data data)
    {
        ID = data.id;
        Name = data.name;
        //Icon = SpriteAtlasManager.Instance.GetSprite(data.iconName); //Resources.Load<Sprite>("Sprites/ItemIcons/Icons/128px/" + data.iconName);
        GetItemSprite(data.iconName);
        Description = data.description;
        ItemType = data.itemType;
        Stackable = data.stackable;
        Value = data.value;
        armourData = data.itemJson;
    }

    public Armour_Item()//For clearing out an items Data when unequiping
    {
        this.ID = -1;
        this.Name = "";
        this.Description = "";
        //Stats/ItemType
        this.Stackable = false;
        this.Value = -1;
        this.armourData = null;
    }

    public void ArmourData()
    {

    }
}
