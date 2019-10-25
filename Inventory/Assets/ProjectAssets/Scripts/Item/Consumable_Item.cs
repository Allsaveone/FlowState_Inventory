using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class Consumable_Item : Item {

    [SerializeField]
    public string consumableData;

    public Consumable_Item(Item_Data data)
    {
        ID = data.id;
        Name = data.name;
        Icon = Resources.Load<Sprite>("Sprites/ItemIcons/Icons/128px/" + data.iconName);
        Description = data.description;
        ItemType = data.itemType;
        Stackable = data.stackable;
        Value = data.value;
        consumableData = data.itemJson;
    }

    public void ConsumableData()
    {

    }

    public void Consume()
    {
        Debug.LogError("Nom.");
    }
}
