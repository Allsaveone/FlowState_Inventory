using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Equipment : MonoBehaviour {

    public Inventory inventory;
    public EquipmentSlot_Helper draggedSlot;

    [Header("Equiped Items:")]
    public Head_Item equipedHead = new Head_Item();
    public Armour_Item equipedArmour = new Armour_Item();
    public Trinket_Item equipedTrinket = new Trinket_Item();
    public WeaponItem equipedWeapon = new WeaponItem();
	
	public void LoadItems () {
	    //load saved items and display the right ui details.
	}

    /// <summary>
    /// SetItemData
    /// used by Equiplot.Equip
    /// </summary>
    /// <param name="equipedItem"></param>
    public void SetItemData(Item equipedItem)
    {
        if (equipedItem.ItemType == ItemType.Weapon)
        {
            if (equipedItem is WeaponItem)
            {
                WeaponItem weapon = (WeaponItem)equipedItem;
                equipedWeapon = weapon;
            }
        }
        else if (equipedItem.ItemType == ItemType.Head)
        {
            //Debug.Log(equipedItem.ItemType + );
            Head_Item headItem = (Head_Item)equipedItem;
            equipedHead = headItem;
        }
        else if (equipedItem.ItemType == ItemType.Armour)
        {
            Armour_Item armour = (Armour_Item)equipedItem;
            equipedArmour = armour;
        }
        else if (equipedItem.ItemType == ItemType.Trinket)
        {
            Trinket_Item trinket = (Trinket_Item)equipedItem;
            equipedTrinket = trinket;
        }
    }

    public void UnEquipByType(Item equipedItem)
    {
        if (equipedItem.ItemType == ItemType.Weapon)
        {
            if (equipedItem is WeaponItem)
            {
                equipedWeapon = new WeaponItem();
            }
        }
        else if (equipedItem.ItemType == ItemType.Head)
        {
            equipedHead = new Head_Item();
        }
        else if (equipedItem.ItemType == ItemType.Armour)
        {
            equipedArmour = new Armour_Item();
        }
        else if (equipedItem.ItemType == ItemType.Trinket)
        {
            equipedTrinket = new Trinket_Item();
        }

        equipedItem = new Item();//blank item

    }

    public Item GetItemData(ItemType type)
    {
        Item item = null;

        switch (type)
        {
            case ItemType.Weapon:
                item = equipedWeapon;
                break;
            case ItemType.Armour:
                item = equipedArmour;
                break;
            case ItemType.Head:
                item = equipedHead;
                break;
            case ItemType.Trinket:
                item = equipedTrinket;
                break;
            default:
                break;
        }

        return item;
    }

    public void DoTest()
    {
        Item item = GetItemData(ItemType.Head);

        string itemJson = JsonUtility.ToJson(item);
        Debug.LogError(itemJson);
    }
}
