using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class InventorySlot : MonoBehaviour ,IDropHandler {

    public int slotId;
    public Inventory inventory;

    public void OnDrop(PointerEventData eventData)
    {
        InventorySlot_Helper draged_slotHelper = eventData.pointerDrag.GetComponentInChildren<InventorySlot_Helper>();//being dragged

        if (draged_slotHelper != null)
        {
            DragDropFromInventory(draged_slotHelper);
            return;
        }  
        
        EquipmentSlot_Helper draged_equipHelper = eventData.pointerDrag.GetComponentInChildren<EquipmentSlot_Helper>();

        if (draged_equipHelper != null)
        {
            InventorySlot_Helper this_slotHelper = GetComponentInChildren<InventorySlot_Helper>();
            draged_equipHelper.DragDropFromEquiped(this_slotHelper);
            return;
        }
    }

    void DragDropFromInventory(InventorySlot_Helper draged_slotHelper)
    {
        if (inventory.items[slotId].ID == -1)
        {
            InventorySlot_Helper this_slotHelper = this.gameObject.GetComponentInChildren<InventorySlot_Helper>();//being droped on

            draged_slotHelper.transform.SetParent(this.gameObject.transform);
            draged_slotHelper.transform.position = this.gameObject.transform.position;//this_slotHelper.slotHolder.transform.position;
            draged_slotHelper.slotHolder = transform.gameObject;

            inventory.items[draged_slotHelper.slotId] = new Item();//move items
            inventory.items[slotId] = draged_slotHelper.item;

            int temp__SlotId = draged_slotHelper.slotId;
            draged_slotHelper.slotId = slotId;
            this_slotHelper.slotId = temp__SlotId;

            this_slotHelper.gameObject.transform.SetParent(inventory.inventorySlots[this_slotHelper.slotId].transform);//draged_slotHelper.slotHolder.transform);
            this_slotHelper.gameObject.transform.position = inventory.inventorySlots[this_slotHelper.slotId].transform.position;
            this_slotHelper.slotHolder = inventory.inventorySlots[this_slotHelper.slotId].transform.gameObject;
        }
        else
        {
            InventorySlot_Helper this_slotHelper = this.gameObject.GetComponentInChildren<InventorySlot_Helper>();//being droped on

            draged_slotHelper.transform.SetParent(this.gameObject.transform);
            draged_slotHelper.transform.position = this.gameObject.transform.position;//this_slotHelper.slotHolder.transform.position;
            draged_slotHelper.slotHolder = transform.gameObject;

            if (draged_slotHelper && this_slotHelper)
            {
                inventory.items[draged_slotHelper.slotId] = this_slotHelper.item;//move items
                inventory.items[slotId] = draged_slotHelper.item;

                int temp__SlotId = draged_slotHelper.slotId;
                draged_slotHelper.slotId = slotId;
                this_slotHelper.slotId = temp__SlotId;

                this_slotHelper.gameObject.transform.SetParent(inventory.inventorySlots[this_slotHelper.slotId].transform);//draged_slotHelper.slotHolder.transform);
                this_slotHelper.gameObject.transform.position = inventory.inventorySlots[this_slotHelper.slotId].transform.position;
                this_slotHelper.slotHolder = inventory.inventorySlots[this_slotHelper.slotId].transform.gameObject;
            }
        }
    }
}
