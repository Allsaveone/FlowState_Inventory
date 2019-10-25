using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class EquipmentSlot : MonoBehaviour,IDropHandler {

    //public int slotIndex;
    public ItemType slotType;
    public Item equipedItem;

    public Inventory inventory;
    public Equipment equipment;

    public Image itemSpriteImage;

    void Start()
    {
        equipedItem = new Item();
    }

    public void LoadEquipedItem()//For later
    { }

    public void UnEquipItem()
    {
        if(equipedItem != null)
        {
            SetEquiped(false);
        }
    }

    public void EquipItem(Item item)
    {
        equipment.inventory.RemoveItem(equipment.inventory.items, item); // remove from inventory.

        if (equipedItem.ID != -1)//if somethings already equiped...
        {
            SetEquiped(false);//clear slot
            equipedItem = item;//equipItem
            SetEquiped(true);//Set Ui
        }
        else
        {
            equipedItem = item;
            SetEquiped(true);
        }

        equipment.SetItemData(equipedItem);

        //Do Stuff - Add Stats Etc - Should be handled by the Equipment manager
    }

    void GetItemData()
    {
        if (equipedItem.ItemType == ItemType.Weapon)
        {
            if (equipedItem is WeaponItem)
            {
                WeaponItem weapon = (WeaponItem)equipedItem;
                equipment.equipedWeapon = weapon;
                //Debug.LogError("!!!");
            }
        }
        else if (equipedItem.ItemType == ItemType.Armour)
        {
            Armour_Item armour = (Armour_Item)equipedItem;
            equipment.equipedArmour = armour;
        }
        else if (equipedItem.ItemType == ItemType.Trinket)
        {
            Trinket_Item trinket = (Trinket_Item)equipedItem;
            equipment.equipedTrinket = trinket;
        }
    }

    public void SetEquiped(bool equiped)
    {
        if (equiped)
        {
            itemSpriteImage.sprite = equipedItem.Icon;
            itemSpriteImage.enabled = true;
        }
        else
        {
            if (equipedItem != null)
            {
                itemSpriteImage.enabled = false;//Ui
                itemSpriteImage.sprite = null;

                if (equipedItem.ID != -1)//If not blank add to inventory
                {
                    equipment.inventory.Add_Item(equipedItem);
                }

                equipment.UnEquipByType(equipedItem);
                equipedItem = new Item();//blank Empty generic item
            }                       
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (equipment.inventory.draggedSlot.item != null)
        {
            if (equipment.inventory.draggedSlot.item.ItemType == slotType)
            {
                equipment.inventory.draggedSlot.transform.SetParent(equipment.inventory.draggedSlot.slotHolder.transform);
                EquipItem(equipment.inventory.draggedSlot.item);

                equipment.inventory.draggedSlot = null;
            }
        }
        else
        {
            Debug.Log("ppffffffftttttt!");
        }

    }
}
