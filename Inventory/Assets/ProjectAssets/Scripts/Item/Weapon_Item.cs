using System;
using UnityEngine;
using System.Collections;
[Serializable]
public class WeaponItem : Item
{
    [SerializeField]
    public string weaponData;

    public WeaponItem(Item_Data data)
    {
        ID = data.id;
        Name = data.name;
        //Icon = SpriteAtlasManager.Instance.GetSprite(data.iconName); //Resources.Load<Sprite>("Sprites/ItemIcons/Icons/128px/" + data.iconName);
        GetItemSprite(data.iconName);
        Description = data.description;
        ItemType = data.itemType;
        Stackable = data.stackable;
        Value = data.value;
        weaponData = data.itemJson;

        //WeaponData();
    }

    public WeaponItem()//For clearing out an items Datawhen unequiping
    {
        this.ID = -1;
        this.Name = "";
        this.Description = "";
        //Stats/ItemType
        this.Stackable = false;
        this.Value = -1;
        this.weaponData = null;
    }

    public void WeaponData()
    {
        //Debug.LogError("Yeet!");
    }
}